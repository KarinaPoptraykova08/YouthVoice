using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System.ComponentModel;

namespace YouthVoice.Models
{
    [FirestoreData]
    public class UserRoleMangement
    {
        [FirestoreProperty("role")]
        public string Role { get; set; }

        public string Email { get; set; }

        private FirestoreDb _firestoreDb;

        public UserRoleMangement()
        {
            // Setting the firestore database (used for storing the role of a user)
            var credentials = GoogleCredential.FromFile("wwwroot/firebase-key.json");
            var firestoreClientBuilder = new FirestoreClientBuilder
            {
                Credential = credentials,
            };
            FirestoreClient firestoreClient = firestoreClientBuilder.Build();
            _firestoreDb = FirestoreDb.Create("youthvoice-e4498", firestoreClient);
        }

        public async Task<List<UserRoleMangement>> FetchUsers()
        {
            var users = new List<UserRoleMangement>();

            Query query = _firestoreDb.Collection("users");
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Documents.Any())
            {
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    var user = document.ConvertTo<UserRoleMangement>();
                    user.Email = document.GetValue<string>("email");
                    users.Add(user);
                }
            }

            return users.OrderBy(x => x.Role).ToList();
        }
    }
}
