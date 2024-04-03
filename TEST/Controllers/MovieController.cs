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
        public async Task<IActionResult> GetRecommend(List<string> IdMovie)
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
        public async Task<IActionResult> RemoveHistory(int Id)
        {
            var data = await this._movieService.RemoveHistory(Id);
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
        public async Task<IActionResult> RemoveFavorite(int Id)
        {
            var data = await this._movieService.RemoveFavorite(Id);
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
        public async Task<IActionResult> Update(MovieModal _data,int Id)
        {
            var data = await this._movieService.Update(_data,Id);
            return Ok(data);
        }

        [HttpDelete("delete-movie")]
        public async Task<IActionResult> Remove(int Id)
        {
            var data = await this._movieService.Remove(Id);
            return Ok(data);
        }
    }
}
