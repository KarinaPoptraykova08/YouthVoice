using System.ComponentModel.DataAnnotations.Schema;

namespace YouthVoice.Data
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string? Address { get; set; }
        public string Location { get; set; }
        public string? VirtualMeetingLink { get; set; }
        public string ImagePath { get; set; }

        public int Capacity { get; set; }
        public DateOnly RegistrationDeadline { get; set; }
        public string? RegistrationLink { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string InPersonOrOnline { get; set; }

        public ICollection<OrganisationEvent> OrganisationEvents { get; set; }

        public Event()
        {
            OrganisationEvents = new List<OrganisationEvent>();
        }
    }
}
