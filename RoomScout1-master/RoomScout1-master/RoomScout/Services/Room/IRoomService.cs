using RoomScout.Models;
using RoomScout.Models.Location;

public interface IRoomService
{
    Task<List<RoomLocation>> GetAllRooms();
    Task<List<RoomLocation>> GetRoomsByType(RoomType type);

    Task<List<RoomLocation>> GetAllRoomsWithAmenities(AmenitiesData amenities);
}