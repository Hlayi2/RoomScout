using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Notification
{
    public string Message { get; set; } // Add this property
    public DateTime Timestamp { get; set; } // Add this property
    public bool IsRead { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsExpanded { get; set; }
    public string Icon { get; set; }
    public string Type { get; set; } = "booking_update";
}