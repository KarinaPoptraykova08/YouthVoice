namespace YouthVoice.Data
{
    public class OrganisationEvent
    {
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
