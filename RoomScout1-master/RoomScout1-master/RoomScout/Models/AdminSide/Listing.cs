using RoomScout.Views.AdminSide;
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
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public LocationData Location { get; internal set; }
        public ContactData Contact { get; internal set; }
        public AmenitiesData Amenities { get; set; }
        public List<Rule> Rules { get; internal set; }
        public object Address { get; internal set; }
        public string Id { get; internal set; }
    }
}
