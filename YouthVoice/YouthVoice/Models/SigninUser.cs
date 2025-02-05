using System.ComponentModel.DataAnnotations;

namespace YouthVoice.Models
{
    public class SigninUser
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
