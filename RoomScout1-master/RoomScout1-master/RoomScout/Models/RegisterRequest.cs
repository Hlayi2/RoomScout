using RoomScout.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Models
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public  UserRole Role { get; set; }
        public string PropertyAddress { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
