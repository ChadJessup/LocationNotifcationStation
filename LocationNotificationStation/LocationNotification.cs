using SQLite;
using System.ComponentModel.DataAnnotations;
namespace LocationNotificationStation.Models;

public class LocationNotification
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Description { get; set; } = string.Empty;

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public double? CustomRadius { get; set; }

    public DateTime CreatedAt { get; set; }
    public bool IsEnabled { get; set; }
    public DateTime LastFired { get; set; }

}
