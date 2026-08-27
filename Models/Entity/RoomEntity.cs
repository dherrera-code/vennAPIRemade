using System.ComponentModel.DataAnnotations.Schema;

namespace vennAPIRemade.Models.Entity
{
    public class RoomEntity: BaseEntity
    {
        public required string Title { get; set; }
        public required string Category { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeOnly? GoldenHour { get; set; }
        [NotMapped]
        public bool IsRoomActive => EventDate >= DateOnly.FromDateTime(DateTime.UtcNow);
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        // Add Collection of Users who are members
        public ICollection<RoomMember> Members { get; set; } = [];
    }
}