using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Mvc;
using YouthVoice.Models;

namespace YouthVoice.Controllers
{
    public class AdministratorController : Controller
    {
        private FirestoreDb _firestoreDb;
        private EmailService _emailService;

        public AdministratorController()
        {
            // Setting the firestore database (used for storing the role of a user)
            var credentials = GoogleCredential.FromFile("wwwroot/firebase-key.json");
            var firestoreClientBuilder = new FirestoreClientBuilder
            {
                Credential = credentials,
            };
            FirestoreClient firestoreClient = firestoreClientBuilder.Build();
            _firestoreDb = FirestoreDb.Create("youthvoice-e4498", firestoreClient);

            _emailService = new EmailService();
        }

        public async Task<IActionResult> RoleManaging()
        {
            var viewers = new List<UserModel>();

            Query query = _firestoreDb.Collection("users").WhereEqualTo("role", "viewer");
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (!User.IsInRole("owner"))
            {
                return Forbid();
            }

            if (snapshot.Documents.Any())
            {
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    var user = document.ConvertTo<UserModel>();
                    user.Email = document.GetValue<string>("email");
                    viewers.Add(user);
                }
            }

            return View(viewers);
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToEditor(string email)
        {
            // Query Firestore for the document that has this email
            Query query = _firestoreDb.Collection("users").WhereEqualTo("email", email);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (!snapshot.Documents.Any())
            {
                return NotFound("User not found");
            }

            // Get the first matching document (assuming email is unique)
            DocumentSnapshot userDoc = snapshot.Documents.First();
            DocumentReference userRef = userDoc.Reference;

            // Update the role field
            await userRef.UpdateAsync("role", "editor");

            // Send notification email
            string subject = "Your Role Has Been Updated";
            string body = $"Dear Sir/Madam,<br><br>Your role has been changed to <strong>Editor</strong> in our system.<br><br>Yours sincerely,<br>The team of YouthVoice";
            await _emailService.SendEmailAsync(email, subject, body);

            return RedirectToAction("RoleManaging");
        }
    }
}
