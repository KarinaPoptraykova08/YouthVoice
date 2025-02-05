using Microsoft.AspNetCore.Mvc;
using YouthVoice.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace YouthVoice.Controllers
{
    public class AuthenticationController : Controller
    {
        private FirebaseAuthConfig config;

        public AuthenticationController()
        {
            config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyC0J4YIz_7nwi0UlJ47hAxSLMONNMRMYrk",
                AuthDomain = "youthvoice-e4498.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    // Configure individual providers
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                }
            };
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AuthData authData)
        {
            // Create the client
            var client = new FirebaseAuthClient(config);

            // Create the user
            await client.CreateUserWithEmailAndPasswordAsync(authData.Email, authData.Password, authData.Username);

            // Log in the new user
            var fbAuthLink = await client.SignInWithEmailAndPasswordAsync(authData.Email, authData.Password);

            return RedirectToAction("Register");
        }
    }
}
