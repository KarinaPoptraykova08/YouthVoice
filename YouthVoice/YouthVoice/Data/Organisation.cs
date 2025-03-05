using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YouthVoice.Data
{    
    public class Organisation
    {
        public int OrganisationId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Email { get; set; }
        public string? SocialMedia { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; } 
        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<OrganisationEvent> OrganisationEvents { get; set; }

        public Organisation()
        {
            OrganisationEvents = new List<OrganisationEvent>();
        }
    }
}
