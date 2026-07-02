namespace InfoTrack.Scraping;

public class ScrapperOptions
{
    public const string SectionName = "ScrapperOptions";

    public string BaseUrl { get; set; } = string.Empty;
    public List<string> AllowedLocations { get; set; } = [];
}
