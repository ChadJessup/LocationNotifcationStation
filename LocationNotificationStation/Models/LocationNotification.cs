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

    public bool IsEnabled { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastFired { get; set; }

    public string LatLong => $"{Latitude:F6} {Longitude:F6}";
}
