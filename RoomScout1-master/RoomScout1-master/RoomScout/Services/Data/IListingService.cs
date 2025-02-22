using RoomScout.Models.AdminSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Services.Data
{
    public interface IListingService
    {
        Task<List<Listing>> GetListingsAsync();
        Task<Listing> GetListingByIdAsync(string id);
        event EventHandler<List<Listing>> OnListingsUpdated;
    }
}
