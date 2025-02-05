using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// DTO (Data Transfer Objects): Simplified version of the entities.
// Consists only of data that is necessary for a specific operation.
namespace Core.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class OrganisationDTO
    {
        public string Name { get; set; }
        public string City { get; set; }
        public List<Event> Events { get; set; }
    }

    public class EventDTO
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Hour { get; set; }
        public double Duration { get; set; }
        public string Location { get; set; }
        public string OrganisationName { get; set; }
    }
}
