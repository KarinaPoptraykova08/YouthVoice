namespace YouthVoice.Models
{
    public class NewEvent
    {
        public string? Title { get; set; }
        public DateOnly Date { get; set; }
        public string? Location { get; set; }
        public required byte[] Image { get; set; }
        public string? Description { get; set; }
    }
}
