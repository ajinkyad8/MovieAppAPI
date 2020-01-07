using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAppAPI.Models;

namespace MovieAppAPI.Data
{
    public interface IModeratorRepository
    {
         Task<List<Artist>> GetArtistsToReview();
         Task<Artist> GetArtistToReview(int artistId);
         Task<List<Movie>> GetMoviesToReview();
         Task<Movie> GetMovieToReview(int movieId);
         Task<List<MovieRole>> GetMovieRolesToReview();
         Task<MovieRole> GetMovieRoleToReview(int id);
         Task<List<ArtistActivityLog>> GetArtistActivityLogToReview();
         Task<List<MovieActivityLog>> GetMovieActivityLogToReview();
         Task<List<MovieRoleActivityLog>> GetMovieRoleActivityLogToReview();
         Task<List<ArtistPhoto>> GetArtistPhotosToReview();
         Task<List<MoviePhoto>> GetMoviePhotosToReview();
         Task<MovieActivityLog> GetMovieActivityToReview(int activityId);
         Task<ArtistActivityLog> GetArtistActivityToReview(int activityId);
         Task<MovieRoleActivityLog> GetMovieRoleActivityToReview(int id);
         Task<List<ArtistDeleteRequest>> GetArtistDeleteRequests();
         Task<ArtistDeleteRequest> GetArtistDeleteRequest(int id);
         Task<List<MovieDeleteRequest>> GetMovieDeleteRequests();
         Task<MovieDeleteRequest> GetMovieDeleteRequest(int id);
         Task<List<MovieRoleDeleteRequest>> GetMovieRoleDeleteRequests();
         Task<MovieRoleDeleteRequest> GetMovieRoleDeleteRequest(int id);
         Task<PhotoDeleteRequest> GetPhotoDeleteRequest(int id);
         Task<List<ArtistPhoto>> GetArtistPhotosWithDeleteRequests();
         Task<List<MoviePhoto>> GetMoviePhotosWithDeleteRequests();
         Task<List<PhotoDeleteRequest>> GetPhotoDeleteRequests(int photoId);
    }
}