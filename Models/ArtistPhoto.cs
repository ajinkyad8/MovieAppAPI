namespace MovieAppAPI.Models
{
    public class ArtistPhoto
    {
        public Photo Photo { get; set; }
        public int PhotoId { get; set; }
        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
        public bool IsArtistApproved { get; set; }
    }
}