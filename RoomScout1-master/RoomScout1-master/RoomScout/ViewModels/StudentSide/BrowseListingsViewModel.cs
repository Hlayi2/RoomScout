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
                    Id = "Gate 1",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Single,  
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Price = "1000.00",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }
                },

                // Add more mock rooms as needed
                new RoomLocation
                {
                    Id = "Gate 2",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Single,  
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Price = "1200.00",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }

                },
                new RoomLocation
                {
                    Id = "Gate 3",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Single,  
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Price = "1600.00",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }

                },

                // Bachelor Rooms
                new RoomLocation
                {
                    Id = "Gate 1",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Bachelor,
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Price = "1600.00",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }

                },
                new RoomLocation
                {
                    Id = "Gate 2",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Bachelor,
                    Price = "1200.00",
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }

                },
                new RoomLocation
                {
                    Id = "Gate 3",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Bachelor,
                    Price = "1900.00",
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",                    
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }

                 },
                // Sharing Rooms 
                new RoomLocation
                {
                    Id = "Gate 1",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Sharing,
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Price = "2000.00",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }
                },
                new RoomLocation
                {
                    Id = "Gate 2",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Sharing,
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Price = "37000.00",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }
                },
                new RoomLocation
                {
                    Id = "Gate 3",
                    Title = "Cozy Single Room",
                    Type = Models.Location.RoomType.Sharing,
                    Address = "123 Main St",
                    ImageUrl = "room_image.png",
                    Price = "2500.00",
                    Description = "Comfortable single room near campus",
                    Amenities = new List<string> { "WiFi", "Furnished" }
                },
            };

        }
    }
}