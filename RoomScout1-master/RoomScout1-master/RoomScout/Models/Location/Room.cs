namespace RoomScout.Models
{
    public enum RoomType
    {
        Bachelor,
        Single,
        Sharing
    }

    public class Room
    {
        internal string Location;

        public string Id { get; set; }
        public string Title { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public string FormattedPrice => $"R {Price:N2} / Month";
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<string> Amenities { get; set; }
        public string LandlordName { get; set; }
        public string LandlordPhone { get; set; }
    }
}