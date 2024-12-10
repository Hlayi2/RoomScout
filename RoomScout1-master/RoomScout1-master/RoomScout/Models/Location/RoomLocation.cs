
namespace RoomScout.Models.Location
{
    public class RoomLocation
    {
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Price { get; set; }
        public string Address { get; set; }
        public RoomType Type { get; set; }
        public string LandlordName { get; set; }
        public string LandlordPhone { get; set; }
    }

    public enum RoomType
    {
        Bachelor,
        Single,
        Sharing
    }
}