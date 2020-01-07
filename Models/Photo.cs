using System.Collections.Generic;

namespace MovieAppAPI.Models
{
    public class Photo
    {        
        public int Id { get; set; }
        public string URL { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
        public bool IsApproved { get; set; }
        public User AddedByUser { get; set; }
        public int AddedByUserId { get; set; }
        public User ApprovedByModerator { get; set; }
        public int? ApprovedByModeratorId { get; set; }
        public ICollection<PhotoDeleteRequest> DeleteRequests { get; set; }
    }
}