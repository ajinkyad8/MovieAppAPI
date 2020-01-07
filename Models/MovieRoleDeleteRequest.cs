namespace MovieAppAPI.Models
{
    public class MovieRoleDeleteRequest
    {        
        public MovieRole MovieRole { get; set; }
        public int MovieRoleId { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}