namespace Mod_Manager.Models;

public class DataMod
{
    // Mod details
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? ModDescription { get; init; }
    public string? Author { get; init; }
    public string? Version { get; init; }
    public DateTime ReleaseDate { get; init; }

    // Mod page information
    public string? DownloadUrl { get; set; }
    public List<string>? ImageUrls { get; init; }

    // Mod status
    public bool IsInstalled { get; set; }
    public bool IsEnabled { get; set; }

    // Mod tags or categories
    public List<string> Tags { get; set; }
}