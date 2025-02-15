using Firebase.Auth;
using System;
using System.Threading.Tasks;
using RoomScout.Services.Auth;

namespace RoomScout.Services
{
    public class AuthService
    {
        // Explicitly use Firebase.Auth.FirebaseConfig
        private readonly FirebaseAuthProvider _authProvider = new FirebaseAuthProvider(
             new FirebaseConfig("AIzaSyBDCTC30yoZRMFqBEuM_vAL5Y1uqkwE4Bo")  
         );

        public async Task<string> RegisterUserAsync(string email, string password, string role)
        {
            try
            {
                var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                string userId = auth.User.LocalId;

                // Store role in SecureStorage (Temporary)
                await SecureStorage.SetAsync("UserRole", role);

                return userId;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            try
            {
                var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
                string userId = auth.User.LocalId;

                // Retrieve role from SecureStorage
                string role = await SecureStorage.GetAsync("UserRole");

                return role;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task LogoutAsync()
        {
            await SecureStorage.SetAsync("UserRole", "");
        }
    }
}