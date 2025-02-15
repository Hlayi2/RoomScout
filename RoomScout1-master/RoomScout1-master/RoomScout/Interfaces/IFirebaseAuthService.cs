using Google.Apis.Auth.OAuth2;
using RoomScout.Models;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Interfaces
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseAuthLink> RegisterWithEmailAndPasswordAsync(string email, string password);
        Task<FirebaseAuthLink> SignInWithEmailAndPasswordAsync(string email, string password);
        Task SendPasswordResetEmailAsync(string email);
        Task SignOutAsync();
        Task<string> GetCurrentUserIdAsync();
        Task RegisterWithEmailAndPasswordAsync(string v, string password, string selectedRole);
       
    }

}

