using System;

namespace MovieAppAPI.Models
{
    public class MovieActivityLog
    {
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public int Runtime { get; set; }
        public string PlotSummary { get; set; }
        public bool IsApproved { get; set; }
        public bool IsMovieApproved { get; set; }
        public User AddedByUser { get; set; }
        public int AddedByUserId { get; set; }
        public User ApprovedByModerator { get; set; }
        public int? ApprovedByModeratorId { get; set; }
        public DateTime TimeStamp { get; set; }
        public MovieActivityLog()
        {
            TimeStamp = DateTime.Now;
        }
    }
}