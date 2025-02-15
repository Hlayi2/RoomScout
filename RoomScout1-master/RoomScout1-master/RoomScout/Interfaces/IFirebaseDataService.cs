using RoomScout.Models;
using System.Threading.Tasks;

namespace RoomScout.Interfaces
{
    public interface IFirebaseDataService
    {
        Task<UserProfile> GetUserProfileAsync(string userId);
        Task CreateUserProfileAsync(string userId, RegisterRequest request);
    }
}