using FirebaseAdmin.Auth;
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
        private FirebaseAuth _fbAuth;

        private UserRoleMangement userRoleManagement;

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

            _fbAuth = FirebaseAuth.DefaultInstance;

            _emailService = new EmailService();

            userRoleManagement = new UserRoleMangement();
        }

        public async Task<IActionResult> AdminDashboard()
        {
            if (!User.IsInRole("owner"))
            {
                return Forbid();
            }

            int userCount = await GetUsersCountAsync();
            return View(userCount);
        }


        public async Task<int> GetUsersCountAsync()
        {
            var users = await _fbAuth.ListUsersAsync(null).ToListAsync();

            return users.Count;
        }

        public async Task<IActionResult> RoleManaging()
        {
            if (!User.IsInRole("owner"))
            {
                return Forbid();
            }

            try
            {
                var fetchViewers = await userRoleManagement.FetchUsers();
                return View(fetchViewers);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Възникна грешка при извличането на потребителите от базата от данни.";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToEditor(string email)
        {
            // Query Firestore for the document that has this email
            Query query = _firestoreDb.Collection("users").WhereEqualTo("email", email);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (!snapshot.Documents.Any())
            {
                return NotFound("Потребителят не е намерен");
            }

            // Get the first matching document (assuming email is unique)
            DocumentSnapshot userDoc = snapshot.Documents.First();
            DocumentReference userRef = userDoc.Reference;

            // Update the role field
            await userRef.UpdateAsync("role", "editor");

            // Send notification email
            string subject = "Вашата роля беше променена";
            string body = $"Уважаеми господине/госпожо,<br><br>Уведомяваме Ви, че вашата роля беше променена в <strong>Editor/Moderator</strong> в нашата система.<br><br>С уважение,<br>Екипът на YouthVoice";
            _emailService.SendEmail(email, subject, body);

            return RedirectToAction("RoleManaging");
        }

        [HttpPost]
        public async Task<IActionResult> DemoteToViewer(string email)
        {
            // Query Firestore for the document that has this email
            Query query = _firestoreDb.Collection("users").WhereEqualTo("email", email);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (!snapshot.Documents.Any())
            {
                return NotFound("Потребителят не е намерен");
            }

            // Get the first matching document (assuming email is unique)
            DocumentSnapshot userDoc = snapshot.Documents.First();
            DocumentReference userRef = userDoc.Reference;

            // Update the role field
            await userRef.UpdateAsync("role", "viewer");

            // Send notification email
            string subject = "Вашата роля беше променена";
            string body = $"Уважаеми господине/госпожо,<br><br>Уведомяваме Ви, че вашата роля беше променена във <strong>Viewer</strong> в нашата система.<br><br>С уважение,<br>Екипът на YouthVoice";
            _emailService.SendEmail(email, subject, body);

            return RedirectToAction("RoleManaging");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string email)
        {
            try
            {
                // Delete the user from Firebase Authentication
                var user = await _fbAuth.GetUserByEmailAsync(email);
                await _fbAuth.DeleteUserAsync(user.Uid);

                // Delete the user's role document from Firestore
                Query query = _firestoreDb.Collection("users").WhereEqualTo("email", email);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                foreach (var document in snapshot.Documents)
                {
                    await document.Reference.DeleteAsync();
                }

                return RedirectToAction("RoleManaging");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = $"Възникна грешка при премахване на потребител: {email}";
                return RedirectToAction("RoleManaging");
            }
        }
    }
}
