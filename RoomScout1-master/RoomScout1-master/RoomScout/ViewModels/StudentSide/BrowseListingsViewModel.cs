using System.Collections.ObjectModel;
using RoomScout.Models.AdminSide;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Compatibility;
using RoomScout.Views.AdminSide;
using RoomScout.Models;
using RoomScout.Models.Location;
using System.Diagnostics;
using LocationRoomType = RoomScout.Models.Location.RoomType;

namespace RoomScout.ViewModels.StudentSide
{
    public partial class BrowseListingsViewModel : ObservableObject
    {
        private readonly IRoomService _roomService;
        private List<RoomLocation> _allRooms;

        [ObservableProperty]
        private ObservableCollection<RoomLocation> _rooms;

        [ObservableProperty]
        private RoomType? _selectedType;

        [ObservableProperty]
        private bool _hasWifi;

        [ObservableProperty]
        private bool _hasFreeElectricity;

        [ObservableProperty]
        private bool _hasBed;

        [ObservableProperty]
        private bool _hasWashingMachine;

        [ObservableProperty]
        private bool _hasStudyTable;

        [ObservableProperty]
        private bool _hasShowers;

        [ObservableProperty]
        private bool _isLoading;

        public BrowseListingsViewModel(IRoomService roomService)
        {
            _roomService = roomService;
            _rooms = new ObservableCollection<RoomLocation>();
            InitializeData();
        }

        private async void InitializeData()
        {
            try
            {
                _isLoading = true;
                _allRooms = await _roomService.GetAllRooms();
                Rooms = new ObservableCollection<RoomLocation>(_allRooms);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data: {ex.Message}");
                // Handle error appropriately
            }
            finally
            {
                _isLoading = false;
            }
        }

        [RelayCommand]
        private async Task RefreshRooms()
        {
            try
            {
                _isLoading = true;
                await ApplyFilters();
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task ApplyFilters()
        {
            var filteredRooms = _allRooms;

            // Apply room type filter
            if (SelectedType.HasValue)
            {
                filteredRooms = await _roomService.GetRoomsByType((LocationRoomType)SelectedType.Value);
            }

            // Apply amenities filter
            if (HasWifi || HasFreeElectricity || HasBed || HasWashingMachine || HasStudyTable || HasShowers)
            {
                var amenitiesFilter = new AmenitiesData
                {
                    Wifi = HasWifi,
                    FreeElectricity = HasFreeElectricity,
                    Bed = HasBed,
                    WashingMachine = HasWashingMachine,
                    StudyTable = HasStudyTable,
                    Showers = HasShowers
                };

                filteredRooms = await _roomService.GetAllRoomsWithAmenities(amenitiesFilter);
            }

            Rooms = new ObservableCollection<RoomLocation>(filteredRooms);
        }

        // Property changed handlers
        partial void OnSelectedTypeChanged(RoomType? value) => _ = ApplyFilters();
        partial void OnHasWifiChanged(bool value) => _ = ApplyFilters();
        partial void OnHasFreeElectricityChanged(bool value) => _ = ApplyFilters();
        partial void OnHasBedChanged(bool value) => _ = ApplyFilters();
        partial void OnHasWashingMachineChanged(bool value) => _ = ApplyFilters();
        partial void OnHasStudyTableChanged(bool value) => _ = ApplyFilters();
        partial void OnHasShowersChanged(bool value) => _ = ApplyFilters();

        [RelayCommand]
        private void ClearFilters()
        {
            SelectedType = null;
            HasWifi = false;
            HasFreeElectricity = false;
            HasBed = false;
            HasWashingMachine = false;
            HasStudyTable = false;
            HasShowers = false;
        }
    }
}