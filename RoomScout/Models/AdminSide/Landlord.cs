using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Models.AdminSide
{
    public class Landlord
    {
        public string FullNames { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactDetails { get; set; } = string.Empty;
        public string IdOrPassportNo { get; set; } = string.Empty;
        public string AccommodationName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = "profiles.png";
    }
}
