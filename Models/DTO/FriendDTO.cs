
namespace vennAPIRemade.Models.DTO
{
    public class FriendDTO
    {
        public int RequesterId { get; set; }
        public int ReceiverId { get; set; }
        public int Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }    
        public UserDTO? Requester { get; set; }
    }
}