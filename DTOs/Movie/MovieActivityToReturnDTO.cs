using System;

namespace MovieAppAPI.DTOs.Movie
{
    public class MovieActivityToReturnDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public int Runtime { get; set; }
        public string PlotSummary { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserName { get; set; }
    }
}