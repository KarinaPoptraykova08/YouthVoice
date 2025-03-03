using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YouthVoice.Models
{
    public class User
    {
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        private readonly string FirebaseApiKey = "AIzaSyC0J4YIz_7nwi0UlJ47hAxSLMONNMRMYrk";

        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={FirebaseApiKey}";

            var payload = new
            {
                requestType = "PASSWORD_RESET",
                email = email,
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            }
        }
    }
}
