
namespace RoomScout.Views.AdminSide
{
    public class AmenitiesData
    {
        public bool Wifi { get; set; }
        public bool FreeElectricity { get; set; }
        public bool Bed { get; set; }
        public bool WashingMachine { get; set; }
        public bool StudyTable { get; set; }
        public bool Showers { get; set; }
        public bool Chair { get; set; }
        public bool Parking { get; set; }




        public static implicit operator Dictionary<object, object>(AmenitiesData v)
        {
            throw new NotImplementedException();
        }
    }
}