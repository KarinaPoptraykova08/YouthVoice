using System.ComponentModel.DataAnnotations;

namespace YouthVoice.Data
{
    public class Organisation
    {
        public int OrganisationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
