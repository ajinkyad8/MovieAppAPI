using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAppAPI.Helpers;
using MovieAppAPI.Models;

namespace MovieAppAPI.Data
{
    public class AppRepository : IAppRepository
    {
        private readonly DataContext dataContext;
        public AppRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;

        }
        public void Add<T>(T entity) where T : class
        {
            dataContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            dataContext.Remove(entity);
        }

        public async Task<Artist> GetArtist(int id)
        {
            return await dataContext.Artists.Include("ActivityLogs").Include("MovieRoles.Movie.MoviePhotos.Photo").Include("MovieRoles.RoleType").Include("ArtistPhotos.Photo").FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Artist> GetArtist(int id, int userId)
        {
            return await dataContext.Artists.IgnoreQueryFilters().Include("ActivityLogs").Include("MovieRoles.Movie.MoviePhotos.Photo")
            .Include("MovieRoles.RoleType").Include("ArtistPhotos.Photo")
            .FirstOrDefaultAsync(a => a.Id == id && a.ActivityLogs.FirstOrDefault().AddedByUserId == userId);
        }

        public async Task<List<ArtistActivityLog>> GetArtistActivityLog(int artistId)
        {
            return await dataContext.ArtistActivityLogs.Include("AddedByUser").Where(a => a.ArtistId == artistId).ToListAsync();
        }

        public async Task<ArtistPhoto> GetArtistDisplayPhoto(int artistId)
        {
            return await dataContext.ArtistPhotos.Include("Photo").FirstOrDefaultAsync(a => a.ArtistId==artistId && a.Photo.IsMain);
        }

        public async Task<ArtistPhoto> GetArtistPhoto(int artistId, int id)
        {
            return await dataContext.ArtistPhotos.Include("Photo").IgnoreQueryFilters().FirstOrDefaultAsync(ap => ap.Artist.Id==artistId && ap.Photo.Id==id);
        }

        public async Task<PaginatedList<Artist>> GetArtists(QueryParams queryParams)
        {
            var artists = dataContext.Artists.Include("ArtistPhotos.Photo").AsQueryable();
            artists = FilterArtists(artists, queryParams);
            return await PaginatedList<Artist>.CreateAsync(artists, queryParams.pageNumber, queryParams.PageSize);
        }

        public async Task<PaginatedList<Artist>> GetArtists(int userId, QueryParams queryParams)
        {            
            var artists = dataContext.Artists.IgnoreQueryFilters().Include("ArtistPhotos.Photo")
            .Where(a => a.IsApproved || (!a.IsApproved && a.ActivityLogs.FirstOrDefault().AddedByUserId == userId)).AsQueryable();
            artists = FilterArtists(artists, queryParams);
            return await PaginatedList<Artist>.CreateAsync(artists, queryParams.pageNumber, queryParams.PageSize);
        }

        private IQueryable<Artist> FilterArtists(IQueryable<Artist> artists, QueryParams queryParams)
        {
            if (!String.IsNullOrEmpty(queryParams.search))
            {
                var searchs = queryParams.search.Trim().Split(' ');
                artists = artists.Where(a => searchs.Any(val => a.FirstName.ToUpper().Contains(val.ToUpper()) || a.LastName.ToUpper().Contains(val.ToUpper()))).AsQueryable();
            }
            if (!queryParams.descending)
            {
                switch (queryParams.sortBy)
                {
                    case "name": 
                    {
                        artists = artists.OrderBy(a => a.FirstName);
                        break;
                    }
                    case "age":
                    {
                        artists = artists.OrderByDescending(a => a.DateOfBirth);
                        break;
                    }
                    case "recent": 
                    {
                        artists = artists.OrderByDescending(a => a.ActivityLogs.FirstOrDefault().TimeStamp);
                        break;
                    }
                    default:
                    {
                        artists = artists.OrderBy(a => a.FirstName);
                        break;
                    }
                }
            }
            else
            {   
                switch (queryParams.sortBy)
                {
                    case "name": 
                    {
                        artists = artists.OrderByDescending(a => a.FirstName);
                        break;
                    }
                    case "age":
                    {
                        artists = artists.OrderBy(a => a.DateOfBirth);
                        break;
                    }
                    case "recent": 
                    {
                        artists = artists.OrderBy(a => a.ActivityLogs.FirstOrDefault().TimeStamp);
                        break;
                    }
                    default:
                    {
                        artists = artists.OrderByDescending(a => a.FirstName);
                        break;
                    }
                }
            }
            return artists;
        }

        public async Task<Movie> GetMovie(int id)
        {
            
            return await dataContext.Movies.Include("ActivityLogs").Include("MovieRoles.Artist.ArtistPhotos.Photo").Include("MovieRoles.RoleType").Include("MoviePhotos.Photo").FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> GetMovie(int id, int userId)
        {
            return await dataContext.Movies.IgnoreQueryFilters().Include("ActivityLogs").Include("MovieRoles.Artist.ArtistPhotos.Photo").Include("MovieRoles.RoleType").Include("MoviePhotos.Photo")
            .FirstOrDefaultAsync(m => m.Id == id && m.ActivityLogs.FirstOrDefault().AddedByUserId==userId);
        }

        public async Task<List<MovieActivityLog>> GetMovieActivityLog(int movieId)
        {
            return await dataContext.MovieActivityLogs.Include("AddedByUser").Where(m => m.MovieId == movieId).ToListAsync();
        }

        public async Task<MoviePhoto> GetMovieDisplayPhoto(int movieId)
        {
            return await dataContext.MoviePhotos.Include("Photo").FirstOrDefaultAsync(m => m.MovieId==movieId && m.Photo.IsMain);
        }

        public async Task<MoviePhoto> GetMoviePhoto(int artistId, int id)
        {
            return await dataContext.MoviePhotos.Include("Photo").IgnoreQueryFilters().FirstOrDefaultAsync(mp => mp.Movie.Id==artistId && mp.Photo.Id==id);
        }

        public async Task<MovieRole> GetMovieRole(int id)
        {
            return await dataContext.MovieRoles.Include("ActivityLogs").Include("Artist").Include("Movie").Include("RoleType").FirstOrDefaultAsync(mr => mr.Id == id);
        }

        public async Task<MovieRole> GetMovieRole(int id, int userId)
        {
            return await dataContext.MovieRoles.IgnoreQueryFilters().Include("ActivityLogs").FirstOrDefaultAsync(mr => mr.Id == id && mr.ActivityLogs.FirstOrDefault().AddedByUserId == userId);
        }

        public async Task<MovieRole> GetMovieRoleByParams(int movieId, int artistId, int roleTypeId)
        {
            return await dataContext.MovieRoles.FirstOrDefaultAsync(m => m.MovieId == movieId && m.ArtistId == artistId && m.RoleTypeId == roleTypeId);
        }

        public async Task<MovieRole> GetMovieRoleByParams(int movieId, int artistId, int roleTypeId, int userId)
        {
            return await dataContext.MovieRoles.IgnoreQueryFilters().
            FirstOrDefaultAsync(m => m.MovieId == movieId && m.ArtistId == artistId && m.RoleTypeId == roleTypeId && m.ActivityLogs.FirstOrDefault().AddedByUserId == userId);
        }

        public async Task<List<MovieRole>> GetMovieRoles()
        {
            return await dataContext.MovieRoles.ToListAsync();
        }

        public async Task<PaginatedList<Movie>> GetMovies(QueryParams queryParams)
        {
            var movies = dataContext.Movies.Include("MoviePhotos.Photo").AsQueryable();
            movies = FilterMovies(movies, queryParams);
            return await PaginatedList<Movie>.CreateAsync(movies, queryParams.pageNumber, queryParams.PageSize);
        }

        public async Task<PaginatedList<Movie>> GetMovies(int userId, QueryParams queryParams)
        {
            var movies = dataContext.Movies.IgnoreQueryFilters().Include("MoviePhotos.Photo").
            Where(m =>m.IsApproved ||  (!m.IsApproved && m.ActivityLogs.FirstOrDefault().AddedByUserId==userId)).AsQueryable();
            movies = FilterMovies(movies, queryParams);
            return await PaginatedList<Movie>.CreateAsync(movies, queryParams.pageNumber, queryParams.PageSize);
        }

        private IQueryable<Movie> FilterMovies(IQueryable<Movie> movies, QueryParams queryParams)
        {
            if (!String.IsNullOrEmpty(queryParams.search))
            {
                movies = movies.Where(a => a.Name.ToUpper().Contains(queryParams.search.ToUpper())).AsQueryable();
            }
            if (!queryParams.descending)
            {
                switch (queryParams.sortBy)
                {
                    case "name": 
                    {
                        movies = movies.OrderBy(m => m.Name);
                        break;
                    }
                    case "release":
                    {
                        movies = movies.OrderByDescending(m => m.ReleaseDate);
                        break;
                    }
                    case "recent": 
                    {
                        movies = movies.OrderByDescending(m => m.ActivityLogs.FirstOrDefault().TimeStamp);
                        break;
                    }
                    default:
                    {
                        movies = movies.OrderBy(m => m.Name);
                        break;
                    }
                }
            }
            else
            {   
                switch (queryParams.sortBy)
                {
                    case "name": 
                    {
                        movies = movies.OrderByDescending(m => m.Name);
                        break;
                    }
                    case "release":
                    {
                        movies = movies.OrderBy(m => m.ReleaseDate);
                        break;
                    }
                    case "recent": 
                    {
                        movies = movies.OrderBy(m => m.ActivityLogs.FirstOrDefault().TimeStamp);
                        break;
                    }
                    default:
                    {
                        movies = movies.OrderByDescending(m => m.Name);
                        break;
                    }
                }
            }
            return movies;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await dataContext.Photos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Photo> GetPhoto(int id, int userId)
        {
            return await dataContext.Photos.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id && p.AddedByUserId == userId);
        }

        public async Task<RoleType> GetRoleType(int id)
        {
            return await dataContext.RoleTypes.FirstOrDefaultAsync(r => r.Id ==id);
        }

        public async Task<List<RoleType>> GetRoleTypes()
        {
            return await dataContext.RoleTypes.ToListAsync();
        }

        public async Task<List<MovieRole>> GetUserArtistMovieRoles(int artistId, int userId)
        {
            return await dataContext.MovieRoles.IgnoreQueryFilters().Include("Movie.MoviePhotos.Photo").Include("RoleType")
            .Where(mr => !mr.IsApproved && mr.ArtistId == artistId && mr.ActivityLogs.FirstOrDefault().AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<ArtistPhoto>> GetUserArtistPhotos(int artistId, int userId)
        {
            return await dataContext.ArtistPhotos.Include("Photo").IgnoreQueryFilters()
            .Where(ap => !ap.Photo.IsApproved && ap.ArtistId == artistId && ap.Photo.AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<MovieRole>> GetUserMovieMovieRoles(int movieId, int userId)
        {
            return await dataContext.MovieRoles.IgnoreQueryFilters().Include("Artist.ArtistPhotos.Photo").Include("RoleType")
            .Where(mr => !mr.IsApproved && mr.MovieId == movieId && mr.ActivityLogs.FirstOrDefault().AddedByUserId == userId).ToListAsync();
        }

        public async Task<List<MoviePhoto>> GetUserMoviePhotos(int movieId, int userId)
        {
            return await dataContext.MoviePhotos.Include("Photo").IgnoreQueryFilters()
            .Where(mp => !mp.Photo.IsApproved && mp.MovieId == movieId && mp.Photo.AddedByUserId == userId).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await dataContext.SaveChangesAsync() > 0 ;
        }

        public async Task<ArtistActivityLog> GetArtistActivity(int artistActivityId)
        {
            return await dataContext.ArtistActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == artistActivityId);
        }

        public async Task<MovieActivityLog> GetMovieActivity(int movieActivityId)
        {
            return await dataContext.MovieActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == movieActivityId);
        }

        public async Task<MovieRoleActivityLog> GetMovieRoleActivity(int movieRoleActivityId)
        {
            return await dataContext.MovieRoleActivityLogs.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == movieRoleActivityId);
        }
    }

}