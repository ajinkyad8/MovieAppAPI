using System;
using System.Collections.Generic;

namespace MovieAppAPI.Models
{
    public class Movie
    {
        public ICollection<MovieRole> MovieRoles { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public int Runtime { get; set; }
        public string PlotSummary { get; set; }
        public bool IsApproved { get; set; }
        public ICollection<MoviePhoto> MoviePhotos { get; set; }
        public ICollection<MovieActivityLog> ActivityLogs { get; set; }
        public ICollection<MovieDeleteRequest> DeleteRequests { get; set; }
    }
}