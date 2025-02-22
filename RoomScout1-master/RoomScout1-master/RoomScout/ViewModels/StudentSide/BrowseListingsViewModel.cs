using System.Collections.ObjectModel;
using RoomScout.Models.AdminSide;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Compatibility;
using RoomScout.Views.AdminSide;
using RoomScout.Models;
using RoomScout.Models.Location;

namespace RoomScout.ViewModels.StudentSide
{
    public partial class BrowseListingsViewModel : ObservableObject
    {
        private readonly IRoomService _roomService;
        private List<RoomLocation> _allRooms;
        private ObservableCollection<RoomLocation> Forms;
        [ObservableProperty]
        private ObservableCollection<RoomLocation> _rooms;

        [ObservableProperty]
        private RoomType? _selectedType;

        public BrowseListingsViewModel(IRoomService roomService)
        {
            _roomService = roomService;
            InitializeData();
        }

        private async void InitializeData()
        {
            _allRooms = await _roomService.GetRoomsAsync();
            Forms = new ObservableCollection<RoomLocation>(_allRooms);
        }

       // [RelayCommand]
      //  private void FilterRooms(RoomType? type)
     //       SelectedType = type;
        //    Rooms = type == null
        //        ? new ObservableCollection<RoomLocation>(_allRooms)
       //         : new ObservableCollection<RoomLocation>(_allRooms.Where(r => r.Type == type.Value));
      //  }
    }

    public interface IRoomService
    {
        Task<List<RoomLocation>> GetRoomsAsync();
    }

    public class MockRoomService : IRoomService
    {
        public Task<List<RoomLocation>> GetRoomsAsync()
        {
            return Task.FromResult(new List<RoomLocation>
            {
                //mock data here
                new RoomLocation
                {
                    Id = "Gate 1",
                    Title = "Cozy Single Room",
                    Type = (Models.Location.RoomType)RoomType.Single,
                    Address = "123 Main St",
                    Price = "1000.00",
                    Images = new List<string> { "single10.png", "single11.png" },
                    Description = "Comfortable single room near campus",
                    Amenities = new AmenitiesData { Wifi = true, BackUpElectricty = true ,Bed = true } ,
                },
                // Add other rooms following the same pattern
                new RoomLocation
                {
                    Id = "Gate 1",
                    Title = "Bachelor Suite",
                    Type = (Models.Location.RoomType)RoomType.Bachelor,
                    Address = "456 Oak St",
                    Price = "1600.00",
                    Images = new List<string> { "img10.png", "img11.png" },
                    Description = "Spacious bachelor apartment",
                    Amenities = new AmenitiesData {Bed = true, Chair= true, FreeElectricity = true }
                }
            });
        }
    }
}
