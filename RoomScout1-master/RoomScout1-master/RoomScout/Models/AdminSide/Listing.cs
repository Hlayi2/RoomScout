using RoomScout.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Models.AdminSide
{
    internal class Listing
    {
        public string Key { get; set; }
        public string RoomType { get; set; }
        public List<string> Images { get; set; }
       // public string Address { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public LocationData Address { get; internal set; }
        public ContactData Contact { get; internal set; }
        public AmenitiesData Amenities { get; internal set; }
        public List<Rule> Rules { get; internal set; }
    }
}
