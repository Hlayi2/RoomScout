using RoomScout.Models;
using RoomScout.Models.Location;
using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;
using RoomScout.Models.AdminSide;

public class RoomService : IRoomService
{
    private readonly FirebaseClient _firebase;

    public RoomService()
    {
        _firebase = new FirebaseClient("https://roomscout-a194c-default-rtdb.firebaseio.com/");
        Debug.WriteLine("RoomService initialized");
    }

    public async Task<List<RoomLocation>> GetAllRooms() 
    { 
        try
        {
            var firebaseListings = (await _firebase
                .Child("listings")
                .OnceAsync<Listing>())
                .Select(item => new RoomLocation
                {
                    Id = item.Key,
                    Type = ConvertToRoomType(item.Object.RoomType),
                    Images = item.Object.Images,
                    Price = item.Object.Price.ToString("F2"),
                    location = $"{item.Object.Address?.Street}, {item.Object.Address?.Suburb}",
                    Amenities = item.Object.Amenities ?? new AmenitiesData(),
                    LocationData = item.Object.Address ?? new LocationData(),
                    Contact = item.Object.Contact ?? new ContactData(),
                    Rules = item.Object.Rules ?? new List<Rule>(),
                    DateAdded = item.Object.DateAdded
                })
                .ToList();

            return firebaseListings;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
            return new List<RoomLocation>();
        }
    }


    public async Task<List<RoomLocation>> GetRoomsByType(RoomType type)
    {
        try
        {
            Debug.WriteLine($"Fetching rooms of type: {type}");
            var rooms = await _firebase
                .Child("listings")
                .OrderBy("RoomType")
                .EqualTo(type.ToString())
                .OnceAsync<RoomLocation>();

            var roomsList = rooms.Select(room =>
            {
                var roomData = room.Object;
                roomData.Id = room.Key;
                return roomData;
            }).ToList();

            Debug.WriteLine($"Found {roomsList.Count} rooms of type {type}");
            return roomsList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR in GetRoomsByType: {ex.Message}");
            return new List<RoomLocation>();
        }
    }

    public async Task<List<RoomLocation>> GetAllRoomsWithAmenities(AmenitiesData amenities)
    {
        try
        {
            Debug.WriteLine("Fetching rooms with specified amenities...");
            Debug.WriteLine($"Amenities requested: Wifi={amenities.Wifi}, " +
                          $"FreeElectricity={amenities.FreeElectricity}, " +
                          $"Bed={amenities.Bed}, " +
                          $"WashingMachine={amenities.WashingMachine}, " +
                          $"StudyTable={amenities.StudyTable}, " +
                          $"Showers={amenities.Showers}");

            var allRooms = await GetAllRooms();
            var filteredRooms = allRooms.Where(room =>
                (!amenities.Wifi || room.Amenities.Wifi) &&
                (!amenities.FreeElectricity || room.Amenities.FreeElectricity) &&
                (!amenities.Bed || room.Amenities.Bed) &&
                (!amenities.WashingMachine || room.Amenities.WashingMachine) &&
                (!amenities.StudyTable || room.Amenities.StudyTable) &&
                (!amenities.Showers || room.Amenities.Showers)
            ).ToList();

            Debug.WriteLine($"Found {filteredRooms.Count} rooms matching amenities criteria out of {allRooms.Count} total rooms");
            return filteredRooms;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR in GetAllRoomsWithAmenities: {ex.Message}");
            return new List<RoomLocation>();
        }
    }
    private RoomType ConvertToRoomType(string roomType)
    {
        return roomType switch
        {
            "Single" => RoomType.Single,
            "Bachelor" => RoomType.Bachelor,
            "Sharing" => RoomType.Sharing,
            _ => throw new ArgumentException($"Unknown room type: {roomType}")
        };
    }
}
