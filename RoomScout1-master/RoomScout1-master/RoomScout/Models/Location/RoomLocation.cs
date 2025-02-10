
namespace RoomScout.Models.Location
{
    public class RoomLocation
    {
        internal string Location;
       // internal string RoomType;

        public string Title { get; set; }

        public string RoomType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Price { get; set; }
        public string FormattedPrice => $"R {Price:N2} / Month";
        public string Address { get; set; }
        public string Status { get; set; }

        public static RoomType type { get; set; }
        public string LandlordName { get; set; }
        public string LandlordPhone { get; set; }
        public string Id { get; internal set; }
        
        public string ImageUrl { get; internal set; }
        public string Description { get; set; }
        public List<string> Amenities { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public ViewModels.StudentSide.RoomType Type { get; internal set; }
    }

        public enum RoomType
        {
            Single,
            Bachelor,
            Sharing
        }
    }

