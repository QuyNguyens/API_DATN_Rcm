using BE_Movie_Rcm.Modal;
using System.Collections.Generic;
using TEST.Helper;
using TEST.Modal;

namespace TEST.Service
{
    public interface IMovieService
    {
        /// <summary>
        /// Get all movie
        /// </summary>
        /// <returns></returns>
        Task<List<MovieModal>> Getall();

        /// <summary>
        /// Get all movie by country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        Task<List<MovieModal>> GetbyCountry(string country);

        /// <summary>
        /// Get all country
        /// </summary>
        /// <returns></returns>
        Task<List<CountryModal>> GetlistCountry();

        /// <summary>
        /// Get the movie recommend when login
        /// </summary>
        /// <param name="IdMovie"></param>
        /// <returns></returns>
        Task<List<MovieModal>> GetRecommend(List<string> IdMovie);

        /// <summary>
        /// Get all the movie by Genre
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        Task<List<MovieModal>> GetbyGenre(string genre);

        /// <summary>
        /// Get all genre
        /// </summary>
        /// <returns></returns>
        Task<List<GenreModal>> GetlistGenre();

        /// <summary>
        /// Get movie by Id when click movie to watch
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<MovieModal> GetbyId(int Id);

        /// <summary>
        /// Get all the movie that user had watched.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<MovieModal>> GetHistory(int IdUser);

        /// <summary>
        /// Add the movie to history.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiReponse> AddHistory(MovieAccessModal data);

        /// <summary>
        /// Remothe the movie by id that user had watched.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiReponse> RemoveHistory(int IdMovie);

        /// <summary>
        /// Remove all the movie that user had watched.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiReponse> RemoveAllHistory(int IdUser);

        /// <summary>
        /// Get all the movie that user had watched.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<MovieModal>> GetFavorite(int IdUser);

        /// <summary>
        /// Add the movie to favorite.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiReponse> AddFavorite(FavoriteModal data);

        /// <summary>
        /// Remothe the movie by id that user had favorite.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiReponse> RemoveFavorite(int IdMovie);

        /// <summary>
        /// Remove all the movie that user had favorite.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiReponse> RemoveAllFavorite(int idUser);

        /// <summary>
        /// Delete the movie
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiReponse> Remove(int Id);

        /// <summary>
        /// Create the movie
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<ApiReponse> Create(MovieCreateModal data);

        /// <summary>
        /// Update the movie
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiReponse> Update(MovieModal data,int Id);

    }
}
