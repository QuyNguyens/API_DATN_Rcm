using TEST.Modal;
using TEST.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BE_Movie_Rcm.Modal;

namespace TEST.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //[AllowAnonymous]
        [HttpGet("get-list-movie")]
        public async Task<IActionResult> Get()
        {
            var data = await this._movieService.Getall();
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("get-by-country")]
        public async Task<IActionResult> GetbyContry(string country)
        {
            var data = await this._movieService.GetbyCountry(country);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("get-list-country")]
        public async Task<IActionResult> GetlistCountry()
        {
            var data = await this._movieService.GetlistCountry();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("get-by-genre")]
        public async Task<IActionResult> GetbyGenre(string genre)
        {
            var data = await this._movieService.GetbyGenre(genre);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("get-list-genre")]
        public async Task<IActionResult> GetlistGenre()
        {
            var data = await this._movieService.GetlistGenre();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("get-movie-recommend")]
        public async Task<IActionResult> GetRecommend(List<int> IdMovie)
        {
            var data = await this._movieService.GetRecommend(IdMovie);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("get-movie-access")]
        public async Task<IActionResult> GetHistory(int Id)
        {
            var data = await this._movieService.GetHistory(Id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("create-history")]
        public async Task<IActionResult> AddHistory(MovieAccessModal _data)
        {
            var data = await this._movieService.AddHistory(_data);
            return Ok(data);
        }

        [HttpDelete("delete-history")]
        public async Task<IActionResult> RemoveHistory(int MovieId, int UserId)
        {
            var data = await this._movieService.RemoveHistory(MovieId, UserId);
            return Ok(data);
        }

        [HttpDelete("delete-all-history")]
        public async Task<IActionResult> RemoveAllHistory(int Id)
        {
            var data = await this._movieService.RemoveAllHistory(Id);
            return Ok(data);
        }

        [HttpGet("get-favorite")]
        public async Task<IActionResult> GetFavorite(int Id)
        {
            var data = await this._movieService.GetFavorite(Id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("create-favorite")]
        public async Task<IActionResult> AddFavorite(FavoriteModal _data)
        {
            var data = await this._movieService.AddFavorite(_data);
            return Ok(data);
        }

        [HttpDelete("delete-favorite")]
        public async Task<IActionResult> RemoveFavorite(int MovieId, int UserId )
        {
            var data = await this._movieService.RemoveFavorite(MovieId, UserId);
            return Ok(data);
        }

        [HttpDelete("delete-all-favorite")]
        public async Task<IActionResult> RemoveAllFavorite(int Id)
        {
            var data = await this._movieService.RemoveAllFavorite(Id);
            return Ok(data);
        }

        [HttpGet("get-movie")]
        public async Task<IActionResult> GetbyId(int Id)
        {
            var data = await this._movieService.GetbyId(Id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        //[AllowAnonymous]
        [HttpPost("create-movie")]
        public async Task<IActionResult> Create(MovieCreateModal _data)
        {
            var data = await this._movieService.Create(_data);
            return Ok(data);
        }

        [HttpPut("update-movie")]
        public async Task<IActionResult> Update(MovieCreateModal _data)
        {
            var data = await this._movieService.Update(_data);
            return Ok(data);
        }

        [HttpDelete("delete-movie")]
        public async Task<IActionResult> Remove(int Id)
        {
            var data = await this._movieService.Remove(Id);
            return Ok(data);
        }

        [HttpPost("create-rating")]
        public async Task<IActionResult> CreateRating(RatingModal _data)
        {
            var data = await this._movieService.Rating(_data);
            return Ok(data);
        }

        [HttpGet("get-access-time")]
        public async Task<IActionResult> GetAccessTime(int userId)
        {
            var data = await this._movieService.GetAccessTime(userId);
            return Ok(data);
        }

        [HttpPost("create-access-time")]
        public async Task<IActionResult> CreateAccessTime(AccessTimeModal _data)
        {
            var data = await this._movieService.CreateAccessTime(_data);
            return Ok(data);
        }

        [HttpPut("update-user-profile")]
        public async Task<IActionResult> UpdateUserProfile(UserProfile userProfile)
        {
            var data = await this._movieService.UpdateUserProfile(userProfile);
            return Ok(data);
        }

        [HttpPut("change-user-password")]
        public async Task<IActionResult> ChangeUserPassword(UserProfile userProfile)
        {
            var data = await this._movieService.ChangePasswordUser(userProfile);
            return Ok(data);
        }

        [HttpPut("update-buy-vip")]
        public async Task<IActionResult> UpdateBuyVip(UpdateUserSubModal data)
        {
            var _data = await this._movieService.UpdateBuyVip(data);
            return Ok(_data);
        }

        [HttpGet("get-group-country")]
        public async Task<IActionResult> GetGroupCountry()
        {
            var _data = await this._movieService.GetGroupCountry();
            return Ok(_data);
        }

        [HttpGet("get-movie-admin")]
        public async Task<IActionResult> GetMovieAdmin()
        {
            var _data = await this._movieService.GetMovieAdmin();
            return Ok(_data);
        }

        [HttpGet("get-user-admin")]
        public async Task<IActionResult> CountUser()
        {
            var _data = await this._movieService.CountUser();
            return Ok(_data);
        }

        [HttpGet("get-user-subs-admin")]
        public async Task<IActionResult> CountUserSubs()
        {
            var _data = await this._movieService.CountUserSubs();
            return Ok(_data);
        }
    }
}
