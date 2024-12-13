using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoomScout.Models.Location;

namespace RoomScout.ViewModels.StudentSide
{
    public partial class BrowseListingsViewModel : ObservableObject
    {
        private readonly IServiceProvider _services;
        private IList<RoomLocation> _allRooms;

        [ObservableProperty]
        private IList<RoomLocation> rooms;

        [ObservableProperty]
        private Models.Location.RoomType? selectedType;  // Specify the exact RoomType

        public BrowseListingsViewModel(IServiceProvider services)
        {
            _services = services;
            _allRooms = MockRoomService();
            Rooms = _allRooms;
        }

        [RelayCommand]
        private void FilterRooms(Models.Location.RoomType? type)  // Specify the exact RoomType
        {
            SelectedType = type;

            if (type == null)
            {
                Rooms = _allRooms;
                return;
            }

            Rooms = _allRooms.Where(r => r.Type == type.Value).ToList();  // Use type.Value
        }

        private List<RoomLocation> MockRoomService()
        {
            return new List<RoomLocation>
            {
                new RoomLocation
                {
                    Id = "1",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Single,  // Use the exact RoomType without casting
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }
                }
                // Add more mock rooms as needed
            };
        }
    }
}