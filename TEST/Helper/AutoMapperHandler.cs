using TEST.Modal;
using BE_Movie_Rcm.Repos.Models;
using AutoMapper;
using BE_Movie_Rcm.Modal;

namespace TEST.Helper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler() {
            CreateMap<TblMovie, MovieModal>().ReverseMap();
            CreateMap<TblMovie, MovieCreateModal>().ReverseMap();
            CreateMap<TblUserMovieAccess, MovieAccessModal>().ReverseMap();
            CreateMap<TblCountry, CountryModal>().ReverseMap();
            CreateMap<TblGenre, GenreModal>().ReverseMap();
            CreateMap<TblFavorite, FavoriteModal>().ReverseMap();
            CreateMap<TblUser, UserModal>().ReverseMap();

        }
    }
}
