namespace MovieAppAPI.DTOs.Photos
{
    public class PhotoForReview
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
        public bool IsApproved { get; set; }
        public string UserName { get; set; }
    }
}