using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAppAPI.Helpers;
using MovieAppAPI.Models;

namespace MovieAppAPI.Data
{
    public interface IAppRepository
    {
         void Add<T>(T entity)where T: class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<Movie> GetMovie(int id);
         Task<Movie> GetMovie(int id, int userId);
         Task<PaginatedList<Movie>> GetMovies(QueryParams queryParams);
         Task<PaginatedList<Movie>> GetMovies(int userId, QueryParams queryParams);
         Task<Artist> GetArtist(int id);
         Task<Artist> GetArtist(int id, int userId);
         Task<PaginatedList<Artist>> GetArtists(QueryParams queryParams);
         Task<PaginatedList<Artist>> GetArtists(int userId, QueryParams queryParams);
         Task<MovieRole> GetMovieRole(int id);
         Task<MovieRole> GetMovieRole(int id, int userId);
         Task<List<MovieRole>> GetMovieRoles();
         Task<RoleType> GetRoleType(int id);
         Task<List<RoleType>> GetRoleTypes();
         Task<ArtistPhoto> GetArtistPhoto(int artistId, int id);
         Task<ArtistPhoto> GetArtistDisplayPhoto(int artistId);
         Task<MoviePhoto> GetMoviePhoto(int movieId, int id);
         Task<MoviePhoto> GetMovieDisplayPhoto(int movieId);
         Task<List<ArtistActivityLog>> GetArtistActivityLog(int artistId);
         Task<List<MovieActivityLog>> GetMovieActivityLog(int movieId);
         Task<List<ArtistPhoto>> GetUserArtistPhotos(int artistId, int userId);
         Task<List<MoviePhoto>> GetUserMoviePhotos(int movieId, int userId);
         Task<List<MovieRole>> GetUserArtistMovieRoles(int artistId, int userId);
         Task<List<MovieRole>> GetUserMovieMovieRoles(int movieId, int userId);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetPhoto(int id, int userId);
         Task<MovieRole> GetMovieRoleByParams(int movieId, int artistId, int roleTypeId);
         Task<MovieRole> GetMovieRoleByParams(int movieId, int artistId, int roleTypeId, int userId);
         Task<ArtistActivityLog> GetArtistActivity(int artistActivityId);
         Task<MovieActivityLog> GetMovieActivity(int movieActivityId);
         Task<MovieRoleActivityLog> GetMovieRoleActivity(int movieRoleActivityId);
    }
}