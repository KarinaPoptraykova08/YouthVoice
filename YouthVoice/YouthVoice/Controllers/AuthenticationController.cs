using Microsoft.AspNetCore.Mvc;
using YouthVoice.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

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

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SigninUser userData)
        {
            try
            {
                // Create the client
                var client = new FirebaseAuthClient(config);

                // Log in the user
                await client.SignInWithEmailAndPasswordAsync(userData.Email, userData.Password);

                // Displaying a greeting
                ViewBag.Message = $"Hello {client.User.Info.DisplayName}";

                return View();
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.Reason)
                {
                    case AuthErrorReason.WrongPassword: ViewBag.PasswordError = "Incorrect password.";
                        break;
                    case AuthErrorReason.UnknownEmailAddress: ViewBag.EmailError = "No user with this email was found.";
                        break;
                    case AuthErrorReason.UserDisabled: ViewBag.Error = "This account has been disabled.";
                        break;

                    default: ViewBag.Error = "Authentication failed.";
                        break;
                }

                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(NewUserCredentials userData)
        {
            try
            {
                // Create the client
                var client = new FirebaseAuthClient(config);

                // Create the user
                await client.CreateUserWithEmailAndPasswordAsync(userData.Email, userData.Password, userData.Username);

                // Log in the new user
                var fbAuthLink = await client.SignInWithEmailAndPasswordAsync(userData.Email, userData.Password);

                // Confirming the successful registration
                ViewBag.Message = "Registration successful!";

                return View();
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.Reason)
                {
                    case AuthErrorReason.EmailExists: ViewBag.EmailError = "The email address is already registered."; 
                        break;
                    case AuthErrorReason.WeakPassword: ViewBag.PasswordError = "The password is too weak. Minimum 6 characters."; 
                        break;

                    default: ViewBag.ErrorMessage = "An error occured"; 
                        break;
                }

                return View();
            }
        }
    }
}
