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
        public string Address { get; set; }
        public string Location { get; set; }
        public string VirtualMeetingLink { get; set; }
        public int MaxCapacity { get; set; }
        public Image FeaturedImage { get; set; }
        public ICollection<Image> AdditionalImages { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public Organisation Organisation { get; set; }
        public string OrganisationName { get; set; }
    }
}
