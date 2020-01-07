using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAppAPI.Models;

namespace MovieAppAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;
        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;

        }
        public async Task<List<ArtistActivityLog>> GetUserArtistActivityLog(int userId)
        {
            return await dataContext.ArtistActivityLogs.Where(a => a.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<ArtistPhoto>> GetUserArtistPhotos(int userId)
        {
            return await dataContext.ArtistPhotos.Include("Photo").Where(a => a.Photo.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<Artist>> GetUserArtists(int userId)
        {
            return await dataContext.Artists.Include("ArtistPhotos.Photo").Where(a => a.ActivityLogs.FirstOrDefault().AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<MovieActivityLog>> GetUserMovieActivityLog(int userId)
        {
            return await dataContext.MovieActivityLogs.Where(a => a.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<MoviePhoto>> GetUserMoviePhotos(int userId)
        {
            return await dataContext.MoviePhotos.Include("Photo").Where(a => a.Photo.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<MovieRoleActivityLog>> GetUserMovieRoleActivityLog(int userId)
        {
            return await dataContext.MovieRoleActivityLogs.Where(a => a.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<MovieRole>> GetUserMovieRoles(int userId)
        {
            return await dataContext.MovieRoles.Include("RoleType").Include("Movie").Include("Artist.ArtistPhotos.Photo").Where(a => a.ActivityLogs.FirstOrDefault().AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<Movie>> GetUserMovies(int userId)
        {
            return await dataContext.Movies.Include("MoviePhotos.Photo").Where(a => a.ActivityLogs.FirstOrDefault().AddedByUserId == userId).ToListAsync();
        }

        public async Task<ArtistActivityLog> GetUserPendingArtistActivity(int userId, int activityId)
        {
            return await dataContext.ArtistActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == activityId && a.AddedByUserId == userId);
        }

        public async Task<List<ArtistActivityLog>> GetUserPendingArtistActivityLog(int userId)
        {
            return await dataContext.ArtistActivityLogs.IgnoreQueryFilters().Where(a =>!a.IsApproved && a.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<ArtistPhoto>> GetUserPendingArtistPhotos(int userId)
        {
            return await dataContext.ArtistPhotos.Include("Photo").IgnoreQueryFilters().Where(a =>!a.Photo.IsApproved && a.Photo.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<Artist>> GetUserPendingArtists(int userId)
        {
            return await dataContext.Artists.IgnoreQueryFilters().Where(a => a.ActivityLogs.FirstOrDefault().AddedByUserId == userId && !a.IsApproved).ToListAsync();
        }

        public async Task<MovieActivityLog> GetUserPendingMovieActivity(int userId, int activityId)
        {
            return await dataContext.MovieActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == activityId && m.AddedByUserId == userId);
        }

        public async Task<List<MovieActivityLog>> GetUserPendingMovieActivityLog(int userId)
        {
            return await dataContext.MovieActivityLogs.IgnoreQueryFilters().Where(a =>!a.IsApproved && a.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<MoviePhoto>> GetUserPendingMoviePhotos(int userId)
        {
            return await dataContext.MoviePhotos.Include("Photo").IgnoreQueryFilters().Where(a =>!a.Photo.IsApproved && a.Photo.AddedByUserId == userId).ToListAsync();
        }

        public async Task<MovieRoleActivityLog> GetUserPendingMovieRoleActivity(int userId, int activityId)
        {
            return await dataContext.MovieRoleActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == activityId && m.AddedByUserId == userId);
        }

        public async Task<List<MovieRoleActivityLog>> GetUserPendingMovieRoleActivityLog(int userId)
        {
            return await dataContext.MovieRoleActivityLogs.IgnoreQueryFilters().Where(a =>!a.IsApproved && a.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<MovieRole>> GetUserPendingMovieRoles(int userId)
        {
            return await dataContext.MovieRoles.IgnoreQueryFilters().Include("RoleType").Include("Movie").Include("Artist.ArtistPhotos.Photo").Where(a => a.ActivityLogs.FirstOrDefault().AddedByUserId == userId && !a.IsApproved).ToListAsync();
        }

        public async Task<List<Movie>> GetUserPendingMovies(int userId)
        {
            return await dataContext.Movies.IgnoreQueryFilters().Where(a => a.ActivityLogs.FirstOrDefault().AddedByUserId == userId && !a.IsApproved).ToListAsync();
        }
    }
}