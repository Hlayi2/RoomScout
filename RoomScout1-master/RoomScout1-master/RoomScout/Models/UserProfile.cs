using RoomScout.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Models
{
    public class UserProfile
    {
        public string Uid { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }  // "Landlord" or "Tenant"
        public DateTime CreatedAt { get; set; }
    }
}
