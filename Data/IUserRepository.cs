using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAppAPI.Models;

namespace MovieAppAPI.Data
{
    public interface IUserRepository
    {
         
         Task<List<Movie>> GetUserMovies(int userId);
         Task<List<Artist>> GetUserArtists(int userId);
         Task<List<MovieRole>> GetUserMovieRoles(int userId);
         Task<List<ArtistPhoto>> GetUserArtistPhotos(int userId);
         Task<List<MoviePhoto>> GetUserMoviePhotos(int userId);
         Task<List<Movie>> GetUserPendingMovies(int userId);
         Task<List<Artist>> GetUserPendingArtists(int userId);
         Task<List<MovieRole>> GetUserPendingMovieRoles(int userId);
         Task<List<ArtistPhoto>> GetUserPendingArtistPhotos(int userId);
         Task<List<MoviePhoto>> GetUserPendingMoviePhotos(int userId);
         Task<List<ArtistActivityLog>> GetUserArtistActivityLog(int userId);
         Task<List<MovieActivityLog>> GetUserMovieActivityLog(int userId);
         Task<List<MovieRoleActivityLog>> GetUserMovieRoleActivityLog(int userId);
         Task<List<ArtistActivityLog>> GetUserPendingArtistActivityLog(int userId);
         Task<List<MovieActivityLog>> GetUserPendingMovieActivityLog(int userId);
         Task<List<MovieRoleActivityLog>> GetUserPendingMovieRoleActivityLog(int userId);
         Task<ArtistActivityLog> GetUserPendingArtistActivity(int userId, int activityId);
         Task<MovieActivityLog> GetUserPendingMovieActivity(int userId, int activityId);
         Task<MovieRoleActivityLog> GetUserPendingMovieRoleActivity(int userId, int activityId);
    }
}