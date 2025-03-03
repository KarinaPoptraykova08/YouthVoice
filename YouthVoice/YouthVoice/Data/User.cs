using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YouthVoice.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
