using Google.Cloud.Firestore;

namespace YouthVoice.Models
{
    [FirestoreData]
    public class UserModel
    {
        [FirestoreProperty("role")]
        public string Role { get; set; }

        public string Email { get; set; }
    }
}
