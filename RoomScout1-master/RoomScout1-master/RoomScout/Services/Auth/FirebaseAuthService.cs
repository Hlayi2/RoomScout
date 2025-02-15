using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using RoomScout.Interfaces;
using RoomScout.Models;
using System;

namespace RoomScout.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private const string ApiKey = "AIzaSyBDCTC30yoZRMFqBEuM_vAL5Y1uqkwE4Bo";
        private const string DatabaseUrl = "https://roomscout-a194c-default-rtdb.firebaseio.com";

        private readonly FirebaseAuthProvider _authProvider;
        private readonly FirebaseClient _firebaseClient;

        public FirebaseAuthService()
        {
            _authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            _firebaseClient = new FirebaseClient(DatabaseUrl);
        }

        public async Task<FirebaseAuthLink> RegisterWithEmailAndPasswordAsync(string email, string password, string role)
        {
            try
            {
                var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);

                var userProfile = new UserProfile
                {
                    Uid = auth.User.LocalId,
                    Email = email,
                    Role = role,
                    CreatedAt = DateTime.UtcNow
                };

                await _firebaseClient
                    .Child("users")
                    .Child(auth.User.LocalId)
                    .PutAsync(userProfile);

                return auth;
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception(GetFriendlyError(ex.Reason.ToString()));
            }
        }

        private string GetFriendlyError(string errorCode)
        {
            return errorCode switch
            {
                "EmailExists" => "Email already registered",
                "WeakPassword" => "Password must be at least 6 characters",
                "InvalidEmailAddress" => "Invalid email format",
                _ => "Registration failed. Please try again"
            };
        }

        public async Task<FirebaseAuthLink> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                return await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception(GetFriendlyError(ex.Reason.ToString()));
            }
        }

        public async Task SendPasswordResetEmailAsync(string email)
        {
            await _authProvider.SendPasswordResetEmailAsync(email);
        }

        public async Task SignOutAsync()
        {
            await SecureStorage.SetAsync("uid", string.Empty);
            await SecureStorage.SetAsync("userRole", string.Empty);
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            return await SecureStorage.GetAsync("uid") ?? string.Empty;
        }
    }
}