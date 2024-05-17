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
using System.Xml.Linq;

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
            var _data = await this._context.TblMovies
                        .Where(movie => movie.IsType == 1)
                        .Take(18)
                        .ToListAsync();
            var _data1 = await this._context.TblMovies
                        .Where(movie => movie.IsType == 2)
                        .Take(18)
                        .ToListAsync();
            _data.AddRange(_data1);
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
                        .Where(ctry => countrys.Contains(ctry.CountryId))
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
            var _data = await this._context.TblCountries.Take(12).ToListAsync();
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
            var _data = await this._context.TblGenres.Take(12).ToListAsync();
            if (_data != null)
            {
                _reponse = this._mapper.Map<List<TblGenre>, List<GenreModal>>(_data);
            }
            return _reponse;
        }

        /// <summary>
        /// Get movie by    end
        /// </summary>
        /// <param name="IdMovie"></param>
        /// <returns></returns>
        public async Task<List<MovieModal>> GetRecommend(List<int> IdMovie)
        {
            MovieModal _reponse = new MovieModal();
            List<MovieModal> _reponse1 = new List<MovieModal>();
            var _data = await this._context.TblMovies
                        .Where(movie => IdMovie.Contains(movie.MovieId))
                        .ToListAsync();

            if (_data != null)
            {
                foreach (var _item in _data)
                {
                    var genres = await this._context.TblGenreMovies
                            .Where(gm => gm.MovieId == _item.MovieId)
                            .Select(gm => gm.Genre.NameGenre)
                            .ToListAsync();

                    _reponse = this._mapper.Map<TblMovie, MovieModal>(_item);
                    _reponse.Genres = genres;
                    _reponse1.Add(_reponse);
                }
            }
            return _reponse1;
        }

        /// <summary>
        /// Get movie by History
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<MovieReponseModal>> GetHistory(int IdUser)
        {
            var _data = await this._context.TblUserMovieAccesses
                        .Where(user => user.UserId == IdUser)
                        .ToListAsync();
            if (_data != null)
            {
                List<MovieReponseModal> _response = new List<MovieReponseModal>();

                foreach (var access in _data)
                {
                    var movies = await this._context.TblMovies
                                        .Where(movie => movie.MovieId == access.MovieId)
                                        .ToListAsync();
                    _response.AddRange((this._mapper.Map<List<TblMovie>, List<MovieReponseModal>>(movies)));
                }
                return _response;
            }
            return null;
        }

        public async Task<ApiReponse> AddHistory(MovieAccessModal data)
        {
            ApiReponse reponse = new ApiReponse();          
            var _data = await this._context.TblUserMovieAccesses
                        .Where(access => access.UserId == data.UserId && access.MovieId == data.MovieId)
                        .FirstOrDefaultAsync();
            if (_data != null)
            {
                reponse.ErroreMessage = "already add in your history";
                return reponse;
            }
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

        public async Task<ApiReponse> RemoveHistory(int MovieId, int UserId)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _movie = this._context.TblUserMovieAccesses
                             .Where(access => access.MovieId == MovieId && access.UserId == UserId)
                             .FirstOrDefault();
                if (_movie != null)
                {
                    this._context.TblUserMovieAccesses.Remove(_movie);
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = MovieId.ToString();
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
        public async Task<List<MovieReponseModal>> GetFavorite(int IdUser)
        {
            var _data = await this._context.TblFavorites
                        .Where(user => user.UserId == IdUser)
                        .ToListAsync();
            if (_data != null)
            {
                List<MovieReponseModal> _response = new List<MovieReponseModal>();

                foreach (var access in _data)
                {
                    var movies = await this._context.TblMovies
                                        .Where(movie => movie.MovieId == access.MovieId)
                                        .ToListAsync();
                    _response.AddRange((this._mapper.Map<List<TblMovie>, List<MovieReponseModal>>(movies)));
                }
                return _response;
            }
            return null;
        }

        public async Task<ApiReponse> AddFavorite(FavoriteModal data)
        {
            ApiReponse reponse = new ApiReponse();
    
            var _data = await this._context.TblFavorites
                        .Where(access => access.UserId == data.UserId && access.MovieId == data.MovieId)
                        .FirstOrDefaultAsync();
            if (_data != null)
            {
                reponse.ErroreMessage = "already add in your favorite";
                return reponse;
            }
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

        public async Task<ApiReponse> RemoveFavorite(int MovieId,int UserId)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _movie = this._context.TblFavorites
                             .Where(access => access.MovieId == MovieId && access.UserId == UserId)
                             .FirstOrDefault();
                if (_movie != null)
                {
                    this._context.TblFavorites.Remove(_movie);
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = MovieId.ToString();
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
        
        public async Task<MovieReponseModal> GetMovieReponse(int Id)
        {
            MovieReponseModal _reponse = new MovieReponseModal();
            var _data = await this._context.TblMovies.FindAsync(Id);
            if (_data != null)
            {
                _reponse = this._mapper.Map<TblMovie, MovieReponseModal>(_data);
            }
            return _reponse;
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
            var actor = (from actor1 in this._context.TblActors
                         join actorMovie in this._context.TblActorMovies on actor1.ActorId equals actorMovie.ActorId into actorGroup
                         select new ActorModal
                         {
                             NameActor = actor1.NameActor,
                             Role = actorGroup.FirstOrDefault().Role,
                         })
                    .Take(6
                    )
                    .ToList();
            var country = await this._context.TblCountryMovies
                        .Where(ctry => ctry.MovieId == Id)
                        .Select(ctry =>  new CountryModal() { 
                            CountryId = ctry.CountryId,
                            NameContry = ctry.Country.NameContry
                        })
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
            var _id = this._context.TblMovies.Max(movie => movie.MovieId);
            data.MovieId = _id+1;
            try
            {
                TblMovie _movie = this._mapper.Map<MovieCreateModal, TblMovie>(data);
                await this._context.TblMovies.AddAsync(_movie);
                await this._context.SaveChangesAsync();
                reponse.ResponseCode = 201;
                reponse.Result = "Oke";
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
        public async Task<ApiReponse> Update(MovieCreateModal data)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _movie = await this._context.TblMovies.FindAsync(data.MovieId);
                if (_movie != null)
                {
                    _movie.Title = data.Title;
                    _movie.Poster = data.Poster;
                    _movie.Descriptions = data.Descriptions;
                    _movie.Urls = data.Urls;
                    _movie.OriginalLanguage = data.OriginalLanguage;
                    _movie.IsType = data.IsType;
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = data.MovieId.ToString();
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
        /// Rating the movie
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiReponse> Rating(RatingModal data)
        {
            ApiReponse reponse = new ApiReponse();

            var _data = this._context.TblRatings
                        .Where(movie => movie.MovieId == data.MovieId && movie.UserId == data.UserId)
                        .FirstOrDefault();
            if(_data != null )
            {
                if(_data.Rating == data.Rating)
                {
                    reponse.ResponseCode = 202;
                    reponse.Result = 202.ToString();
                }
                else
                {
                    _data.Rating = data.Rating;
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = data.MovieId.ToString();
                }
            }
            else
            {
                TblRating _rating = this._mapper.Map<RatingModal, TblRating>(data);
                await this._context.TblRatings.AddAsync(_rating);
                await this._context.SaveChangesAsync();
                reponse.ResponseCode = 201;
                reponse.Result = data.MovieId.ToString();
            }

            return reponse;
        }

        public async Task<ApiReponse> CreateAccessTime(AccessTimeModal data)
        {
            ApiReponse reponse = new ApiReponse();
            var dataFind = this._context.TblAccessTimes.Where(mv => mv.UserId == data.UserId && mv.CountryId == data.CountryId).FirstOrDefault();
            if(dataFind != null)
            {
                dataFind.AccessTime = dataFind.AccessTime + data.AccessTime;
                await this._context.SaveChangesAsync();
                reponse.ResponseCode = 201;
                reponse.Result = data.AccessTime.ToString();
            }
            else
            {
                try
                {
                    TblAccessTime _access = this._mapper.Map<AccessTimeModal, TblAccessTime>(data);
                    await this._context.TblAccessTimes.AddAsync(_access);
                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 201;
                    reponse.Result = data.AccessTime.ToString();
                }
                catch (Exception ex)
                {
                    reponse.ResponseCode = 400;
                    reponse.ErroreMessage = ex.Message;
                }
            }
            return reponse;
        }

        public async Task<AccessTimeResponseModal> GetAccessTime(int userId)
            {
            var _data = await this._context.TblAccessTimes.Where(user => user.UserId == userId)
                .OrderByDescending(t => t.AccessTime)
                .Take(5)
                .ToListAsync();
            var _idCountry = _data.Select(p => p.CountryId).ToList();
            var _accesstime = _data.Select(t => t.AccessTime).ToList();
            var _country = _idCountry
                            .Select(id => this._context.TblCountries.FirstOrDefault(country => country.CountryId == id)?.NameContry)
                            .ToList();
            AccessTimeResponseModal _reponse = new AccessTimeResponseModal
            {
                country = _country!,
                accessTime = _accesstime
            };
            return _reponse;
        }

        public async Task<ApiReponse> UpdateUserProfile(UserProfile userProfile)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _user = await this._context.TblUsers.FindAsync(userProfile.userId);
                if (_user != null)
                {
                    _user.UserName = userProfile.userName;
                    _user.Phone = userProfile.phone;
                    _user.Avatar = userProfile.avatar;
                    _user.Gender = userProfile.gender;

                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = userProfile.userId.ToString();
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

        public async Task<ApiReponse> ChangePasswordUser(UserProfile userProfile)
        {
            ApiReponse reponse = new ApiReponse();
            try
            {
                var _user = await this._context.TblUsers.FindAsync(userProfile.userId);
                if (_user != null)
                {
                    _user.Password = userProfile.newPassword;

                    await this._context.SaveChangesAsync();
                    reponse.ResponseCode = 200;
                    reponse.Result = userProfile.userId.ToString();
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

        public async Task<BuyVipResponseModal> UpdateBuyVip(UpdateUserSubModal data)
        {
            BuyVipResponseModal reponse = new BuyVipResponseModal();
            try
            {
                if(data.status == 0)
                {
                    UserSubModal userSubModal = new UserSubModal()
                    {
                        UserId = data.userId,
                        Status = 1,
                        isType = data.isType,
                        StartDay = DateTime.Now,
                        EndDay = DateTime.Now,
                    };
                    TblUserSub _userSub = this._mapper.Map<UserSubModal, TblUserSub>(userSubModal);
                    await this._context.TblUserSubs.AddAsync(_userSub);
                    await this._context.SaveChangesAsync();
                }
                else
                {
                    var _data = await this._context.TblUserSubs.
                                Where(item => item.IsType == data.isType && item.UserId == data.userId)
                                .FirstOrDefaultAsync();
                    if(_data != null)
                    {
                        int day = 0;
                        switch (data.isType)
                        {
                            case 1:
                            case 4:
                                day = 30; break;                             
                            case 2:
                                day = 90; break;                               
                            case 3:
                                day = 365; break;
                        }
                        _data.Status = 2;
                        _data.StartDay = DateTime.Now;
                        _data.EndDay = DateTime.Now.AddDays(day);
                        await this._context.SaveChangesAsync();
                        reponse.StartDay = DateTime.Now;
                        reponse.EndDay = DateTime.Now.AddDays(day);
                    }
                }
                
            }
            catch (Exception ex)
            {
                reponse.StartDay = DateTime.Now;
                reponse.EndDay = DateTime.Now;
            }
            return reponse;
        }

        public async Task<AccessTimeResponseModal> GetGroupCountry()
        {
            var accessTimeModalList = (from accessTime in this._context.TblCountries
                                       join country in this._context.TblAccessTimes on accessTime.CountryId equals country.CountryId into countryAccessGroup
                                       select new AccessTimeModal
                                       {
                                           CountryId = accessTime.CountryId,
                                           nameCountry = countryAccessGroup.FirstOrDefault().Country.NameContry, // Assuming at least one country record per CountryId
                                           AccessTime = countryAccessGroup.Sum(item => item.AccessTime ?? 0) // Handle null AccessTime
                                       })
                                       .OrderByDescending(item => item.AccessTime)
                                       .Take(7)
                           .ToList();

            var countryAccessData = from country in this._context.TblCountries
                                    join accessTime in this._context.TblAccessTimes on country.CountryId equals accessTime.CountryId into countryAccessGroup
                                    select new AccessTimeModal
                                    {
                                        CountryId = country.CountryId,
                                        nameCountry = country.NameContry,
                                        AccessTime = countryAccessGroup.Count()
                                    };
            var newDataCountry = countryAccessData.OrderByDescending(item => item.AccessTime).Take(7).ToList();
            AccessTimeResponseModal reponse = new AccessTimeResponseModal()
            {
                countUser = newDataCountry,
                sumAccessTime = accessTimeModalList
            };
            return reponse;
        }

        public async Task<MovieAdminResponseModal> GetMovieAdmin()
        {
            MovieAdminResponseModal _responce = new MovieAdminResponseModal();

            //var amount = await this._context.TblMovies.CountAsync();
            int amount = 45300;
            List<MovieModal> listMovie = new List<MovieModal>();

            var _data = this._context.TblMovies.Take(40).ToList();
            listMovie = this._mapper.Map<List<TblMovie>, List<MovieModal>>(_data);

            _responce.Amount = amount;
            _responce.ListMovie = listMovie;
            return _responce;
        }

        public async Task<UserAdminResponseModal> CountUser()
        {
            UserAdminResponseModal _responce = new UserAdminResponseModal();

            var amount = this._context.TblUsers.Count();
            List<UserAdminModal> listUser = new List<UserAdminModal>();

            var _data = this._context.TblUsers.Take(40).ToList();
            listUser = this._mapper.Map<List<TblUser>, List<UserAdminModal> >(_data);

            _responce.Amount = amount;
            _responce.ListUser = listUser;
            return _responce;
        }

        public async Task<UserSubAdminResponse> CountUserSubs()
        {
            UserSubAdminResponse _responce = new UserSubAdminResponse();

            var amount = this._context.TblUserSubs.Where(user => user.Status == 1).Count();
            List<UserSubModal> listUser = new List<UserSubModal>();

            var _data = this._context.TblUserSubs.ToList();
            listUser = this._mapper.Map<List<TblUserSub>, List<UserSubModal>>(_data);

            _responce.Amount = amount;
            _responce.ListUserSubs = listUser;
            return _responce;
        }

        public async Task<List<MovieReponseModal>> GetVoteMovieAdm()
        {
            var topMovies = await this._context.TblMovies
                        .Where(m => m.VoteCount > 1000)
                        .Take(7)
                        .OrderByDescending(m => m.VoteCount)
                        .ToListAsync();
            List<MovieReponseModal> _reponse = new List<MovieReponseModal>();
            _reponse = this._mapper.Map<List<TblMovie>, List<MovieReponseModal>>((List<TblMovie>)topMovies);
            return _reponse;
        }

        public async Task<List<CountryModal>> GetCountryCodeAdm()
        {
            var _data = await this._context.TblCountries.ToListAsync();
            List<CountryModal> _response = new List<CountryModal>();

            _response = this._mapper.Map<List<TblCountry>, List<CountryModal>>(_data);
            return _response;
        }
    }
}
