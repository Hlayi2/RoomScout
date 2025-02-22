namespace RoomScout.Models.Location
{
    public class RoomLocation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public RoomType Type { get; set; }
        public string Address { get; set; }
        public string Price { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }
        public AmenitiesData Amenities { get; set; }
        public LocationData LocationData { get; set; }
        public ContactData Contact { get; set; }
        public List<Rule> Rules { get; set; }
        public DateTime DateAdded { get; set; }
    }

    public class AmenitiesData
    {
        public bool Wifi { get; set; }
        public bool BackUpElectricty { get; set; }
        public bool FreeElectricity { get; set; }
        public bool Bed { get; set; }
        public bool WashingMachine { get; set; }
        public bool StudyTable { get; set; }
        public bool Showers { get; set; }
        public bool Chair { get; set; }
    }

    public class LocationData
    {
        public string Coordinates { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class ContactData
    {
        public string AlternativePhone { get; set; }
    }

    public enum RoomType
    {
        Single,
        Bachelor,
        Sharing
    }
}