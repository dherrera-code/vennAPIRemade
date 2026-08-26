namespace vennAPIRemade.Models.DTO
{
    public class RoomDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeOnly? GoldenHour { get; set; }
        public bool? IsRoomActive { get; set; }
        public int? UserId { get; set; }
        public List<RoomMemberDTO> Members { get; set; } = [];
    }
}