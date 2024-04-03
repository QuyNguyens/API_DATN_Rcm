using TEST.Service;
using TEST.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TEST.Helper;
using System.Collections.Generic;
using Azure;
using BE_Movie_Rcm.Repos.Models;
using BE_Movie_Rcm.Repos;
using System.Linq;
using System.Collections;
using System.Diagnostics.Metrics;
using BE_Movie_Rcm.Modal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TEST.Container
{
    public class MovieService : IMovieService
    {
        private readonly MovieDataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor MovieService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public MovieService(MovieDataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        /// <summary>
        /// The get all movie
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieModal>> Getall()
        {
            List<MovieModal> _reponse = new List<MovieModal>();
            var _data = await this._context.TblMovies.ToListAsync();
            if (_data != null)
            {
                _reponse = this._mapper.Map<List<TblMovie>, List<MovieModal>>(_data);
            }
            return _reponse;
        }

        /// <summary>
        /// Get movie by country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task<List<MovieModal>> GetbyCountry(string country)
        {
            List<MovieModal> _reponse = new List<MovieModal>();
            var countrys = await this._context.TblCountries
                        .Where(ctry => ctry.NameContry == country)
                        .Select(ctry => ctry.CountryId)
                        .ToListAsync();
            var movies = await this._context.TblCountryMovies
                        .Where(ctry => countrys.Contains((int)ctry.CountryId))
                        .Select(ctry => ctry.MovieId)
                        .ToListAsync();

            var _data = await this._context.TblMovies
                        .Where(movie => movies.Contains(movie.MovieId))
                        .Take(18)
                        .ToListAsync();
                        
            if (_data != null)
            {
                _reponse = this._mapper.Map<List<TblMovie>, List<MovieModal>>(_data);
            }
            return _reponse;
        }

        /// <summary>
        /// The get all country
        /// </summary>
        /// <returns></returns>
        public async Task<List<CountryModal>> GetlistCountry()
        {
            List<CountryModal> _reponse = new List<CountryModal>();
            var _data = await this._context.TblCountries.ToListAsync();
            if (_data != null)
            {
                _reponse = this._mapper.Map<List<TblCountry>, List<CountryModal>>(_data);
            }
            return _reponse;
        }

        /// <summary>
        /// Get movie by Genre
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        public async Task<List<MovieModal>> GetbyGenre(string genre)
        {
            List<MovieModal> _reponse = new List<MovieModal>();
            var genres = await this._context.TblGenres
                        .Where(gre => gre.NameGenre == genre)
                        .Select(gre => gre.GenreId)
                        .ToListAsync();
            var movies = await this._context.TblGenreMovies
                        .Where(gre => genres.Contains((int)gre.GenreId))
                        .Select(gre => gre.MovieId)
                        .ToListAsync();

            var _data = await this._context.TblMovies
                        .Where(movie => movies.Contains(movie.MovieId))
                        .Take(18)
                        .ToListAsync();
            if (_data != null)
            {
                _reponse = this._mapper.Map<List<TblMovie>, List<MovieModal>>(_data);
            }
            return _reponse;
        }

        /// <summary>
        /// The get all Genre
        /// </summary>
        /// <returns></returns>
        public async Task<List<GenreModal>> GetlistGenre()
        {
            List<GenreModal> _reponse = new List<GenreModal>();
            var _data = await this._context.TblGenres.ToListAsync();
            if (_data != null)
            {
                _reponse = this._mapper.Map<List<TblGenre>, List<GenreModal>>(_data);
            }
            return _reponse;
        }

        /// <summary>
        /// Get movie by recommend
        /// </summary>
        /// <param name="IdMovie"></param>
        /// <returns></returns>
        public async Task<List<MovieModal>> GetRecommend(List<string> IdMovie)
        {
            List<MovieModal> _reponse = new List<MovieModal>();
            var _data = await this._context.TblMovies
                        .Where(movie => IdMovie.Contains(movie.MovieId.ToString()))
                        .ToListAsync();

            var _data1 = await this._context.TblMovies
                        .Where(movie => movie.IsType == 1)
                        .Take(18)
                        .ToListAsync();

            var _data2 = await this._context.TblMovies
                        .Where(movie => movie.IsType == 2)
                        .Take(18)
                        .ToListAsync();
            _data.AddRange(_data1);
            _data.AddRange(_data2);

            if (_data != null)
            {
                _reponse = this._mapper.Map<List<TblMovie>, List<MovieModal>>(_data);
            }
            return _reponse;
        }

        /// <summary>
        /// Get movie by History
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<MovieModal>> GetHistory(int IdUser)
        {
            var _data = await this._context.TblUserMovieAccesses
                        .Where(user => user.UserId == IdUser)
                        .ToListAsync();
            if (_data != null)
            {
                List<MovieModal> _response = new List<MovieModal>();

                foreach (var access in _data)
                {
                    var movies = await this._context.TblMovies
                                        .Where(movie => movie.MovieId == access.MovieId)
                                        .ToListAsync();

                    _response.AddRange((this._mapper.Map<List<TblMovie>, List<MovieModal>>(movies)));
                }
                return _response;
            }
            return null;
        }

        public async Task<ApiReponse> AddHistory(MovieAccessModal data)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                TblUserMovieAccess _movie = this._mapper.Map<MovieAccessModal, TblUserMovieAccess>(data);
                await this._context.TblUserMovieAccesses.AddAsync(_movie);
                await this._context.SaveChangesAsync();
                reponse.ResponseCode = 201;
                reponse.Result = data.MovieId.ToString();
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }

        public async Task<ApiReponse> RemoveHistory(int Id)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _movie = await this._context.TblUserMovieAccesses.FindAsync(Id);
                if (_movie != null)
                {
                    this._context.TblUserMovieAccesses.Remove(_movie);
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = Id.ToString();
                }
                else
                {
                    reponse.ResponseCode = 404;
                    reponse.ErroreMessage = "Data not found";
                }
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }

        public async Task<ApiReponse> RemoveAllHistory(int IdUser)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var user = this._context.TblUserMovieAccesses.Where(x => x.UserId == IdUser);
                if (user != null)
                {
                    this._context.TblUserMovieAccesses.RemoveRange(user);
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = IdUser.ToString();
                }
                else
                {
                    reponse.ResponseCode = 404;
                    reponse.ErroreMessage = "Data not found";
                }
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }

        /// <summary>
        /// Get movie by Favorite
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<MovieModal>> GetFavorite(int Id)
        {
            var _data = await this._context.TblFavorites
                        .Where(user => user.UserId == Id)
                        .ToListAsync();
            if (_data != null)
            {
                List<MovieModal> _response = new List<MovieModal>();

                foreach (var access in _data)
                {
                    var movies = await this._context.TblMovies
                                        .Where(movie => movie.MovieId == access.MovieId)
                                        .ToListAsync();

                    _response.AddRange((this._mapper.Map<List<TblMovie>, List<MovieModal>>(movies)));
                }
                return _response;
            }
            return null;
        }

        public async Task<ApiReponse> AddFavorite(FavoriteModal data)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                TblFavorite _movie = this._mapper.Map<FavoriteModal, TblFavorite>(data);
                await this._context.TblFavorites.AddAsync(_movie);
                await this._context.SaveChangesAsync();
                reponse.ResponseCode = 201;
                reponse.Result = data.MovieId.ToString();
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }

        public async Task<ApiReponse> RemoveFavorite(int Id)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _movie = await this._context.TblFavorites.FindAsync(Id);
                if (_movie != null)
                {
                    this._context.TblFavorites.Remove(_movie);
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = Id.ToString();
                }
                else
                {
                    reponse.ResponseCode = 404;
                    reponse.ErroreMessage = "Data not found";
                }
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }

        public async Task<ApiReponse> RemoveAllFavorite(int IdUser)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var user = this._context.TblFavorites.Where(x => x.UserId == IdUser);
                if (user != null)
                {
                    this._context.TblFavorites.RemoveRange(user);
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = IdUser.ToString();
                }
                else
                {
                    reponse.ResponseCode = 404;
                    reponse.ErroreMessage = "Data not found";
                }
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }
        /// <summary>
        /// Get movie by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MovieModal> GetbyId(int Id)
        {
            var genres = await this._context.TblGenreMovies
                        .Where(gm => gm.MovieId == Id)
                        .Select(gm => gm.Genre.NameGenre)
                        .ToListAsync();
            var actor = await this._context.TblActorMovies

                        .Where (actor => actor.MovieId == Id)
                        .Select (actor => actor.Actor)
                        .ToListAsync();
            var country = await this._context.TblCountryMovies

                        .Where(ctry => ctry.MovieId == Id)
                        .Select(ctry => ctry.Country.NameContry)
                        .ToListAsync();
            MovieModal _reponse = new MovieModal();
            var _data = await this._context.TblMovies.FindAsync(Id);          
            if (_data != null)
            {
                _reponse = this._mapper.Map<TblMovie, MovieModal>(_data);
                _reponse.Genres = genres;
                _reponse.Actors = actor;
                _reponse.Countrys = country;
            }
            
            return _reponse;
        }

        /// <summary>
        /// Create the movie
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ApiReponse> Create(MovieCreateModal data)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                TblMovie _movie = this._mapper.Map<MovieCreateModal, TblMovie>(data);
                await this._context.TblMovies.AddAsync(_movie);
                await this._context.SaveChangesAsync();
                reponse.ResponseCode = 201;
                reponse.Result = data.MovieId.ToString();
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }

        /// <summary>
        /// Remove the movie
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ApiReponse> Remove(int Id)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _movie = await this._context.TblMovies.FindAsync(Id);
                if (_movie != null) {
                    this._context.TblMovies.Remove(_movie);
                    await this._context.SaveChangesAsync(); 
                    reponse.ResponseCode = 200;
                    reponse.Result = Id.ToString();
                }
                else
                {
                    reponse.ResponseCode = 404;
                    reponse.ErroreMessage = "Data not found";
                }
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }

        /// <summary>
        /// Update the movie
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ApiReponse> Update(MovieModal data, int Id)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _movie = await this._context.TblMovies.FindAsync(Id);
                if (_movie != null)
                {
                    _movie.Title = data.Title;
                    _movie.Status = data.Status;
                    _movie.Poster = data.Poster;
                    _movie.Descriptions = data.Descriptions;
                    _movie.Urls = data.Urls;
                    _movie.VoteCount = data.VoteCount;
                    _movie.VoteAverage = data.VoteAverage;
                    _movie.OriginalLanguage = data.OriginalLanguage;
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = Id.ToString();
                }
                else
                {
                    reponse.ResponseCode = 404;
                    reponse.ErroreMessage = "Data not found";
                }
            }
            catch (Exception ex)
            {
                reponse.ResponseCode = 400;
                reponse.ErroreMessage = ex.Message;
            }
            return reponse;
        }

    }
}
