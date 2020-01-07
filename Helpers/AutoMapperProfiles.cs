using System.Linq;
using AutoMapper;
using MovieAppAPI.DTOs.Artist;
using MovieAppAPI.DTOs.Auth;
using MovieAppAPI.DTOs.Movie;
using MovieAppAPI.DTOs.MovieRole;
using MovieAppAPI.DTOs.Photos;
using MovieAppAPI.Models;

namespace MovieAppAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ArtistForUpdateDTO, Artist>();
            CreateMap<MovieRole, ArtistForMovieRoleDTO>();
            CreateMap<MovieRole, MovieForMovieRoleDTO>();
            CreateMap<ArtistPhoto, ArtistForPhotoDTO>();
            CreateMap<MoviePhoto, MovieForPhotoDTO>();
            CreateMap<MovieForUpdateDTO, Movie>();
            CreateMap<UserForRegisterDTO, User>();
            CreateMap<User, UserForReturnDTO>();
            CreateMap<UserRole, UserRoleToReturnDTO>();
            CreateMap<Role, RoleToReturnDTO>();
            CreateMap<Artist, ArtistActivityLog>().ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<Movie, MovieActivityLog>().ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<ArtistActivityLog, Artist>().ForMember(a => a.Id, opt => opt.Ignore()).
            ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<MovieActivityLog, Movie>().ForMember(m => m.Id, opt => opt.Ignore()).
            ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Artist, ArtistForListDTO>().
            ForMember(ar => ar.PhotoUrl, opt => opt.MapFrom(a => a.ArtistPhotos.FirstOrDefault(ap => ap.Photo.IsMain).Photo.URL)).ReverseMap();
            CreateMap<Artist, ArtistForDetailDTO>().
            ForMember(ar => ar.PhotoUrl, opt => opt.MapFrom(a => a.ArtistPhotos.FirstOrDefault(ap => ap.Photo.IsMain).Photo.URL)).ReverseMap();
            CreateMap<Movie, MovieForListDTO>().
            ForMember(ml => ml.PhotoUrl, opt => opt.MapFrom(m => m.MoviePhotos.FirstOrDefault(ap => ap.Photo.IsMain).Photo.URL));
            CreateMap<Movie, MovieForDetailDTO>().
            ForMember(ml => ml.PhotoUrl, opt => opt.MapFrom(m => m.MoviePhotos.FirstOrDefault(ap => ap.Photo.IsMain).Photo.URL));
            CreateMap<MovieRole, MovieRoleForReviewDTO>().
            ForMember(mr => mr.ArtistName, opt => opt.MapFrom(m => m.Artist.FirstName + " "  + m.Artist.LastName)).
            ForMember(mr => mr.MovieName, opt => opt.MapFrom(m => m.Movie.Name + " (" + m.Movie.ReleaseDate.Year + ")")).            
            ForMember(mr => mr.RoleName, opt => opt.MapFrom(m => m.RoleType.RoleName)).
            ForMember(mr => mr.ArtistPhotoUrl, opt => opt.MapFrom(m => m.Artist.ArtistPhotos.FirstOrDefault(ap => ap.Photo.IsMain).Photo.URL)).
            ForMember(mr => mr.UserName, opt => opt.MapFrom(m => m.ActivityLogs.FirstOrDefault().AddedByUser.UserName));
            CreateMap<MoviePhoto, MoviePhotoForReviewDTO>().          
            ForMember(mp => mp.MovieName, opt => opt.MapFrom(m => m.Movie.Name));
            CreateMap<ArtistPhoto, ArtistPhotoForReviewDTO>().           
            ForMember(ap => ap.ArtistName, opt => opt.MapFrom(a => a.Artist.FirstName + " "  + a.Artist.LastName));
            CreateMap<MovieRoleActivityLog, MovieRole>().ForMember(m => m.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<ArtistDeleteRequest, ArtistForDeleteRequestDTO>();
            CreateMap<MovieDeleteRequest, MovieForDeleteRequestDTO>();
            CreateMap<MovieRoleDeleteRequest, MovieRoleForDeleteRequestDTO>();
            CreateMap<PhotoDeleteRequest, PhotoDeleteRequestsForReviewDTO>();
            CreateMap<ArtistActivityLog, ArtistActivityToReturnDTO>().ForMember(a => a.UserName, opt => opt.MapFrom(aa => aa.AddedByUser.UserName));
            CreateMap<MovieActivityLog, MovieActivityToReturnDTO>().ForMember(m => m.UserName, opt => opt.MapFrom(ma => ma.AddedByUser.UserName));
            CreateMap<Movie, MovieForReviewDTO>().ForMember(m => m.UserName, opt => opt.MapFrom(mo => mo.ActivityLogs.FirstOrDefault().AddedByUser.UserName));
            CreateMap<Artist, ArtistForReviewDTO>().ForMember(a => a.UserName, opt => opt.MapFrom(ar => ar.ActivityLogs.FirstOrDefault().AddedByUser.UserName));
            CreateMap<MovieRoleActivityLog, MovieRoleActivityToReturn>().ForMember(m => m.UserName, opt => opt.MapFrom(mr => mr.AddedByUser.UserName));
            CreateMap<Photo, PhotoForReview>().ForMember(p => p.UserName, opt => opt.MapFrom(ph => ph.AddedByUser.UserName));
        }
    }
}