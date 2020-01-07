using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieAppAPI.Models;

namespace MovieAppAPI.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole,
    IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<MovieRole> MovieRoles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<ArtistPhoto> ArtistPhotos { get; set; }
        public DbSet<MoviePhoto> MoviePhotos { get; set; }
        public DbSet<ArtistActivityLog> ArtistActivityLogs { get; set; }
        public DbSet<MovieActivityLog> MovieActivityLogs { get; set; }
        public DbSet<MovieRoleActivityLog> MovieRoleActivityLogs { get; set; }
        public DbSet<ArtistDeleteRequest> ArtistDeleteRequests { get; set; }
        public DbSet<MovieDeleteRequest> MovieDeleteRequests { get; set; }
        public DbSet<MovieRoleDeleteRequest> MovieRoleDeleteRequests { get; set; }
        public DbSet<PhotoDeleteRequest> PhotoDeleteRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>(userRole =>
            {

                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            builder.Entity<MovieRole>().HasQueryFilter(mr => mr.IsApproved);
            builder.Entity<Artist>().HasQueryFilter(a => a.IsApproved).HasMany(a => a.MovieRoles).WithOne(mr => mr.Artist).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ArtistPhoto>().HasKey(k => new { k.ArtistId, k.PhotoId });
            builder.Entity<Movie>().HasQueryFilter(a => a.IsApproved).HasMany(m => m.MovieRoles).WithOne(mr => mr.Movie).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MoviePhoto>().HasKey(k => new { k.MovieId, k.PhotoId });
            builder.Entity<Photo>().HasQueryFilter(p => p.IsApproved);
            builder.Entity<ArtistActivityLog>().HasQueryFilter(a => a.IsApproved);
            builder.Entity<MovieActivityLog>().HasQueryFilter(m => m.IsApproved);
            builder.Entity<MovieRoleActivityLog>().HasQueryFilter(m => m.IsApproved);
        }
    }
}