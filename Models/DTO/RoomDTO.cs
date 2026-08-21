namespace vennAPIRemade.Models.DTO
{
    public class RoomDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeOnly? ChosenHour { get; set; }
        public bool? IsRoomActive { get; set; } = true;
        public int? UserId { get; set; }
    }
}