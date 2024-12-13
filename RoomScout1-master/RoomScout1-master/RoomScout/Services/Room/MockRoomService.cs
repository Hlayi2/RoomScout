using RoomScout.Models;
using RoomScout.Models.Location;

public class MockRoomService : IRoomService
{
    private readonly List<RoomLocation> _rooms = new()
    {
        new RoomLocation
        {
            Id = "1",
            Type = RoomScout.Models.Location.RoomType.Sharing,
            Price = "R 2500.00",
            Location = "Monkiviweng Hospital",
            ImageUrl = "room_sharing.png",
            Description = "Comfortable sharing room",
            Amenities = new List<string> { "Free WiFi", "Free Consultation" },
            LandlordName = "John Doe",  // Add this
            LandlordPhone = "123-456-7890"  // Add this

        },
        new RoomLocation
        {
            Id = "2",
            Type = RoomScout.Models.Location.RoomType.Bachelor,
            Price = "R 3000.00",
            Location = "Monkiviweng Hospital",
            ImageUrl = "room_bachelor.png",
            Description = "Spacious bachelor unit",
            Amenities = new List<string> { "Free WiFi", "Free Consultation" },
            LandlordName = "Jane Smith",  // Add this
            LandlordPhone = "098-765-4321"  // Add this
        }
    };

    public Task<List<RoomLocation>> GetAllRooms()
    {
        throw new NotImplementedException();
    }

    public List<RoomLocation> GetRooms() => _rooms;

    public Task<List<RoomLocation>> GetRoomsByType(RoomScout.Models.Location.RoomType type)
    {
        throw new NotImplementedException();
    }
}