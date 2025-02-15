using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using RoomScout.Interfaces;
using RoomScout.Models;

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

    public async Task<FirebaseAuthLink> RegisterWithEmailAndPasswordAsync(string email, string password)
    {
        try
        {
            var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            var userProfile = new UserProfile
            {
                Uid = auth.User.LocalId,
                Email = email,
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
            throw new Exception(ex.Message);
        }
    }

    public async Task<FirebaseAuthLink> SignInWithEmailAndPasswordAsync(string email, string password)
    {
        try
        {
            return await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
        }
        catch (FirebaseAuthException ex)
        {
            throw new Exception(ex.Message);
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

    public Task<FirebaseAuthLink> SignInWithEmailPasswordAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task RegisterWithEmailAndPasswordAsync(string v, string password, string selectedRole)
    {
        throw new NotImplementedException();
    }

   
}