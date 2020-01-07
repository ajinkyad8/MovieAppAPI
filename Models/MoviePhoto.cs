namespace MovieAppAPI.Models
{
    public class MoviePhoto
    {
        
        public Photo Photo { get; set; }
        public int PhotoId { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public bool IsMovieApproved { get; set; }
    }
}