namespace MovieAppAPI.Models
{
    public class ArtistDeleteRequest
    {
        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}