using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace YouthVoice.Models
{
    public class AuthData
    {        
        [Required]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        //[Required]
        //public string? PasswordConfirmation { get; set; }
    }
}
