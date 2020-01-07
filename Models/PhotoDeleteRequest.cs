namespace MovieAppAPI.Models
{
    public class PhotoDeleteRequest
    {        
        public Photo Photo { get; set; }
        public int PhotoId { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}