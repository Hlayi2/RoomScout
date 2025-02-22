using RoomScout.Models;
using RoomScout.Models.Location;
using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;

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
            Debug.WriteLine("Fetching all rooms...");
            var rooms = await _firebase
                .Child("listings")
                .OnceAsync<RoomLocation>();

            var roomsList = rooms.Select(room =>
            {
                var roomData = room.Object;
                roomData.Id = room.Key;

                // Debug all nested objects
                Debug.WriteLine($"\nRoom ID: {roomData.Id}");
                Debug.WriteLine($"Title: {roomData.Title}");
                Debug.WriteLine($"Type: {roomData.Type}");
                Debug.WriteLine($"Price: {roomData.Price}");

                // Debug Amenities
                if (roomData.Amenities == null)
                {
                    Debug.WriteLine("Amenities object is null");
                    roomData.Amenities = new AmenitiesData(); // Initialize if null
                }
                else
                {
                    Debug.WriteLine("Amenities:");
                    Debug.WriteLine($"- Wifi: {roomData.Amenities.Wifi}");
                    Debug.WriteLine($"- BackUpElectricty: {roomData.Amenities.BackUpElectricty}");
                    Debug.WriteLine($"- FreeElectricity: {roomData.Amenities.FreeElectricity}");
                    Debug.WriteLine($"- Bed: {roomData.Amenities.Bed}");
                    Debug.WriteLine($"- WashingMachine: {roomData.Amenities.WashingMachine}");
                    Debug.WriteLine($"- StudyTable: {roomData.Amenities.StudyTable}");
                    Debug.WriteLine($"- Showers: {roomData.Amenities.Showers}");
                    Debug.WriteLine($"- Chair: {roomData.Amenities.Chair}");
                }

                // Debug Location
                if (roomData.LocationData == null)
                {
                    Debug.WriteLine("LocationData object is null");
                    roomData.LocationData = new LocationData(); // Initialize if null
                }
                else
                {
                    Debug.WriteLine("Location Data:");
                    Debug.WriteLine($"- Street: {roomData.LocationData.Street}");
                    Debug.WriteLine($"- Suburb: {roomData.LocationData.Suburb}");
                    Debug.WriteLine($"- City: {roomData.LocationData.City}");
                    Debug.WriteLine($"- PostalCode: {roomData.LocationData.PostalCode}");
                    Debug.WriteLine($"- Coordinates: {roomData.LocationData.Coordinates}");
                }

                // Debug Contact
                if (roomData.Contact == null)
                {
                    Debug.WriteLine("Contact object is null");
                    roomData.Contact = new ContactData(); // Initialize if null
                }
                else
                {
                    Debug.WriteLine($"Alternative Phone: {roomData.Contact.AlternativePhone}");
                }

                Debug.WriteLine("------------------------");
                return roomData;
            }).ToList();

            Debug.WriteLine($"Successfully retrieved {roomsList.Count} rooms");
            return roomsList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR in GetAllRooms: {ex.Message}");
            Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
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
}
//using RoomScout.Models;
//using RoomScout.Models.Location;
//using Firebase.Database;
//using Firebase.Database.Query;
//using System.Diagnostics;
//public class RoomService : IRoomService
//{
//    private readonly FirebaseClient _firebase;

//    public RoomService()
//    {
//        _firebase = new FirebaseClient("https://roomscout-a194c-default-rtdb.firebaseio.com/");
//    }

//    public async Task<List<RoomLocation>> GetAllRooms()
//    {
//        try
//        {
//            var rooms = await _firebase
//                .Child("listings")
//                .OnceAsync<RoomLocation>();

//            return rooms.Select(room =>
//            {
//                var roomData = room.Object;
//                roomData.Id = room.Key;
//                return roomData;
//            }).ToList();
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error fetching rooms: {ex.Message}");
//            return new List<RoomLocation>();
//        }
//    }

//    public async Task<List<RoomLocation>> GetRoomsByType(RoomType type)
//    {
//        try
//        {
//            var rooms = await _firebase
//                .Child("listings")
//                .OrderBy("RoomType")
//                .EqualTo(type.ToString())
//                .OnceAsync<RoomLocation>();

//            return rooms.Select(room =>
//            {
//                var roomData = room.Object;
//                roomData.Id = room.Key;
//                return roomData;
//            }).ToList();
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error fetching rooms by type: {ex.Message}");
//            return new List<RoomLocation>();
//        }
//    }

//    public async Task<List<RoomLocation>> GetAllRoomsWithAmenities(AmenitiesData amenities)
//    {
//        try
//        {
//            var allRooms = await GetAllRooms();

//            return allRooms.Where(room =>
//                (!amenities.Wifi || room.Amenities.Wifi) &&
//                (!amenities.FreeElectricity || room.Amenities.FreeElectricity) &&
//                (!amenities.Bed || room.Amenities.Bed) &&
//                (!amenities.WashingMachine || room.Amenities.WashingMachine) &&
//                (!amenities.StudyTable || room.Amenities.StudyTable) &&
//                (!amenities.Showers || room.Amenities.Showers)
//            ).ToList();
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error filtering rooms by amenities: {ex.Message}");
//            return new List<RoomLocation>();
//        }
//    }

//}