using Firebase.Auth;
using System.Threading.Tasks;

namespace RoomScout.Interfaces
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseAuthLink> RegisterWithEmailAndPasswordAsync(string email, string password, string role);
        Task<FirebaseAuthLink> SignInWithEmailAndPasswordAsync(string email, string password);
        Task SendPasswordResetEmailAsync(string email);
        Task SignOutAsync();
        Task<string> GetCurrentUserIdAsync();
    }
}