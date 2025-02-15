using Firebase.Database;
using Firebase.Database.Query;
using RoomScout.Interfaces;
using RoomScout.Models;
using System.Threading.Tasks;

namespace RoomScout.Services.Auth
{
    public class FirebaseDataService : IFirebaseDataService
    {
        private readonly FirebaseClient _firebaseClient = new FirebaseClient("https://roomscout-a194c-default-rtdb.firebaseio.com");

        public async Task<UserProfile> GetUserProfileAsync(string userId)
        {
            return await _firebaseClient
                .Child("users")
                .Child(userId)
                .OnceSingleAsync<UserProfile>();
        }

        public async Task CreateUserProfileAsync(string userId, RegisterRequest request)
        {
            var profile = new UserProfile
            {
                Uid = userId,
                Email = request.Email,
                FullName = request.FullName,
                Role = request.Role.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            await _firebaseClient
                .Child("users")
                .Child(userId)
                .PutAsync(profile);
        }
    }
}