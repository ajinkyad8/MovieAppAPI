namespace MovieAppAPI.Models
{
    public class MovieDeleteRequest
    {
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}