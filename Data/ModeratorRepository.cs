using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAppAPI.Models;

namespace MovieAppAPI.Data
{
    public class ModeratorRepository : IModeratorRepository
    {
        private readonly DataContext dataContext;
        public ModeratorRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;

        }

        public async Task<List<ArtistActivityLog>> GetArtistActivityLogToReview()
        {
            return await dataContext.ArtistActivityLogs.IgnoreQueryFilters().Include("AddedByUser").Where(a => a.IsArtistApproved && !a.IsApproved).ToListAsync();
        }

        public async Task<ArtistActivityLog> GetArtistActivityToReview(int activityId)
        {
            return await dataContext.ArtistActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(aa => aa.Id == activityId);
        }

        public async Task<ArtistDeleteRequest> GetArtistDeleteRequest(int id)
        {
            return await dataContext.ArtistDeleteRequests.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<ArtistDeleteRequest>> GetArtistDeleteRequests()
        {
            return await dataContext.ArtistDeleteRequests.Include("Artist").Include("User").Where(a => !a.IsApproved).ToListAsync();
        }

        public async Task<List<ArtistPhoto>> GetArtistPhotosWithDeleteRequests()
        {
            return await dataContext.ArtistPhotos.Include("Artist").Include("Photo").Where(a => a.Photo.DeleteRequests.Count > 0).ToListAsync();
        }

        public async Task<List<ArtistPhoto>> GetArtistPhotosToReview()
        {
            return await dataContext.ArtistPhotos.Include("Photo.AddedByUser").Include("Artist").IgnoreQueryFilters().Where(ap => ap.IsArtistApproved && !ap.Photo.IsApproved).ToListAsync();
        }

        public async Task<List<Artist>> GetArtistsToReview()
        {
            return await dataContext.Artists.IgnoreQueryFilters().Include("ActivityLogs.AddedByUser").Where(a => !a.IsApproved).ToListAsync();
        }

        public async Task<Artist> GetArtistToReview(int artistId)
        {
            return await dataContext.Artists.Include("ArtistPhotos.Photo").Include("MovieRoles").Include("ActivityLogs").IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == artistId);
        }

        public async Task<List<MovieActivityLog>> GetMovieActivityLogToReview()
        {
            return await dataContext.MovieActivityLogs.IgnoreQueryFilters().Include("AddedByUser").Where(m => m.IsMovieApproved && !m.IsApproved).ToListAsync();
        }

        public async Task<MovieActivityLog> GetMovieActivityToReview(int activityId)
        {
            return await dataContext.MovieActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(ma => ma.Id == activityId);
        }

        public async Task<MovieDeleteRequest> GetMovieDeleteRequest(int id)
        {
            return await dataContext.MovieDeleteRequests.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<MovieDeleteRequest>> GetMovieDeleteRequests()
        {
            return await dataContext.MovieDeleteRequests.Include("Movie").Include("User").Where(m => !m.IsApproved).ToListAsync();
        }

        public async Task<List<MoviePhoto>> GetMoviePhotosWithDeleteRequests()
        {
            return await dataContext.MoviePhotos.Include("Movie").Include("Photo").Where(m => m.Photo.DeleteRequests.Count > 0).ToListAsync();
        }

        public async Task<List<MoviePhoto>> GetMoviePhotosToReview()
        {
            return await dataContext.MoviePhotos.Include("Photo.AddedByUser").Include("Movie").IgnoreQueryFilters().Where(mp => mp.IsMovieApproved && !mp.Photo.IsApproved).ToListAsync();
        }

        public async Task<List<MovieRoleActivityLog>> GetMovieRoleActivityLogToReview()
        {
            return await dataContext.MovieRoleActivityLogs.IgnoreQueryFilters().Include("AddedByUser").Where(mr => mr.IsMovieRoleApproved && !mr.IsApproved).ToListAsync();
        }

        public async Task<MovieRoleActivityLog> GetMovieRoleActivityToReview(int id)
        {
            return await dataContext.MovieRoleActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(mr => mr.Id == id);
        }

        public async Task<MovieRoleDeleteRequest> GetMovieRoleDeleteRequest(int id)
        {
            return await dataContext.MovieRoleDeleteRequests.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<MovieRoleDeleteRequest>> GetMovieRoleDeleteRequests()
        {
            return await dataContext.MovieRoleDeleteRequests.Include("MovieRole.Movie").Include("MovieRole.Artist").Include("MovieRole.RoleType").Include("User").Where(mr => !mr.IsApproved).ToListAsync();
        }

        public async Task<List<MovieRole>> GetMovieRolesToReview()
        {
            return await dataContext.MovieRoles.IgnoreQueryFilters().Include("Movie").Include("Artist").Include("RoleType").Include("ActivityLogs.AddedByUser").Where(mr => mr.IsArtistApproved && mr.IsMovieApproved && !mr.IsApproved).ToListAsync();
        }

        public async Task<MovieRole> GetMovieRoleToReview(int id)
        {
            return await dataContext.MovieRoles.IgnoreQueryFilters().Include("ActivityLogs").FirstOrDefaultAsync(mr =>mr.Id == id);
        }

        public async Task<List<Movie>> GetMoviesToReview()
        {
            return await dataContext.Movies.IgnoreQueryFilters().Include("ActivityLogs.AddedByUser").Where(m => !m.IsApproved).ToListAsync();
        }

        public async Task<Movie> GetMovieToReview(int movieId)
        {
            return await dataContext.Movies.Include("MovieRoles").Include("MoviePhotos.Photo").Include("ActivityLogs").IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id==movieId);
        }

        public async Task<PhotoDeleteRequest> GetPhotoDeleteRequest(int id)
        {
            return await dataContext.PhotoDeleteRequests.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PhotoDeleteRequest>> GetPhotoDeleteRequests(int photoId)
        {
            return await dataContext.PhotoDeleteRequests.Include("User").Where(p => p.PhotoId==photoId).ToListAsync();
        }
    }
}