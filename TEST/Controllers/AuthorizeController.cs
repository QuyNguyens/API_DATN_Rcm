﻿using TEST.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BE_Movie_Rcm.Repos;
using TEST.Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BE_Movie_Rcm.Modal;
using BE_Movie_Rcm.Repos.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure;
using System.Xml.Linq;

namespace TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        private readonly MovieDataContext context;
        private readonly IMapper _mapper;
        public AuthorizeController(MovieDataContext context, IOptions<JwtSettings> options, IMapper mapper1)
        {
            this.context = context;
            this.jwtSettings = options.Value;
            this._mapper = mapper1;
        }

        [HttpPost("check-user")]

        public async Task<IActionResult> GenerateToken([FromBody] UserModal userCred)
        {
            var userEmail = await this.context.TblUsers.FirstOrDefaultAsync(item => item.Email == userCred.Email);
            
            var user = await this.context.TblUsers.FirstOrDefaultAsync(item => item.Email == userCred.Email && item.Password == userCred.Password);
            
            if (user != null)
            {
                //generate token
                //var tokenhandler = new JwtSecurityTokenHandler();
                //var tokenkey = Encoding.UTF8.GetBytes(this.jwtSettings.securityKey);
                //var tokendesc = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(new Claim[]
                //    {
                //        new Claim(ClaimTypes.Name,user.Email),
                //        new Claim(ClaimTypes.Role,user.Role.ToString())
                //    }),
                //    Expires = DateTime.UtcNow.AddSeconds(30000),
                //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                //};
                //var token = tokenhandler.CreateToken(tokendesc);
                //var finaltoken = tokenhandler.WriteToken(token);
                var _reponse = this._mapper.Map<TblUser, UserResponse>(user);
                var userSub = await this.context.TblUserSubs.
                                    Where(item => item.UserId == user.UserId)
                                    .ToListAsync();
                var buyVip = await this.context.TblBuyVips.ToListAsync();
                var _dataBuyVip = this._mapper.Map<List<TblBuyVip>, List<BuyVipModal>>(buyVip);

                _reponse.BuyVips = _dataBuyVip;
                if(userSub != null)
                {
                    var _data = this._mapper.Map<List<TblUserSub>, List<UserSubModal>>(userSub);
                    _reponse.SubModals = _data;
                }

                return Ok(_reponse);
                //return Ok(new TokenResponse() { Token = finaltoken, RefreshToken = await this.refresh.GenerateToken(userCred.username) });

            }
            if(userEmail != null)
            {
               return BadRequest(userEmail);
            }
            else
            {
                
                return BadRequest();
            }
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] UserModal userCred)
        {
            ApiReponse reponse = new ApiReponse();
            reponse.ResponseCode = 400;
            var userId = await this.context.TblUsers.MaxAsync(item => item.UserId);
            userCred.UserId = userId+1;
            var user = await this.context.TblUsers.FirstOrDefaultAsync(item => item.Email == userCred.Email);
            if (user == null)
            {
                try
                {
                    TblUser _user = this._mapper.Map<UserModal, TblUser>(userCred);
                    await this.context.TblUsers.AddAsync(_user);
                    await this.context.SaveChangesAsync();
                    reponse.ResponseCode = userId + 1;
                    reponse.Result = userCred.Email.ToString();
                }
                catch (Exception ex)
                {
                    reponse.ResponseCode = 400;
                    reponse.ErroreMessage = ex.Message;
                }
                return Ok(reponse);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var _user = await this.context.TblUsers.FirstOrDefaultAsync(user => user.UserId == id);

            if(_user != null)
            {
                this.context.TblUsers.Remove(_user);
                await this.context.SaveChangesAsync ();
                return Ok();
            }
            return NotFound();
        }
    }
}
