using Microsoft.AspNetCore.Mvc;
using YouthVoice.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore.V1;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Text;
using Google.Apis.Auth;

namespace YouthVoice.Controllers
{
    public class AuthenticationController : Controller
    {
        private FirebaseAuthClient _firebaseClient;

        private FirestoreDb _firestoreDb;

        private readonly FirebaseAuthService _firebaseAuthService;

        public AuthenticationController()
        {
            // Setting the config
            FirebaseAuthConfig config = new FirebaseAuthConfig
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

            // Creating the client
            _firebaseClient = new FirebaseAuthClient(config);

            // Setting the firestore database (used for storing the role of a user)
            var credentials = GoogleCredential.FromFile("wwwroot/firebase-key.json");
            var firestoreClientBuilder = new FirestoreClientBuilder
            {
                Credential = credentials,
            };
            FirestoreClient firestoreClient = firestoreClientBuilder.Build();
            _firestoreDb = FirestoreDb.Create("youthvoice-e4498", firestoreClient);

            // Setting the reset password survice
            _firebaseAuthService = new FirebaseAuthService();
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
                // Logging in the user
                var userRecord = await _firebaseClient.SignInWithEmailAndPasswordAsync(userData.Email, userData.Password);

                // Fetching an user's role
                var doc = await _firestoreDb.Collection("users").Document(userRecord.User.Uid).GetSnapshotAsync();
                var role = doc.Exists ? doc.GetValue<string>("role") : "viewer";

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userRecord.User.Info.Email),
                    new Claim(ClaimTypes.Name, userRecord.User.Info.DisplayName),
                    new Claim(ClaimTypes.Role, role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Displaying a greeting
                ViewBag.Message = $"Hello {_firebaseClient.User.Info.DisplayName}";

                return RedirectToAction("Index", "Home");
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
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
                // Creating the user
                var userRecords = await _firebaseClient.CreateUserWithEmailAndPasswordAsync(userData.Email, userData.Password, userData.Username);
                
                // Determining the role
                var userDoc = _firestoreDb.Collection("users").Document(userRecords.User.Uid);
                await userDoc.SetAsync(new
                {
                    email = userData.Email,
                    role = "viewer"
                });                

                // Confirming the successful registration
                ViewBag.Message += "Регистрацията е успешна";

                return RedirectToAction("SignIn");
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                switch (ex.Reason)
                {
                    case AuthErrorReason.EmailExists: ViewBag.EmailError = "Имейл адресът вече е бил регистриран."; 
                        break;
                    case AuthErrorReason.WeakPassword: ViewBag.PasswordError = "Паролата е прекалено слаба. Изискват се поне 6 символа, главни и малки букви."; 
                        break;

                    default: ViewBag.ErrorMessage = "Възникна грешка."; 
                        break;
                }

                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
           
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var success = await _firebaseAuthService.SendPasswordResetEmailAsync(email);

            if (success)
            {
                ViewBag.Message = "Password reset email sent. Check your inbox.";
            }
            else
            {
                ViewBag.Message = "Error sending password reset email.";
            }

            return View();
        }
    }
}
