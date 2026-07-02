namespace InfoTrack.Scraping;

public interface IWebScraper
{
    Task<string> GetHtmlAsync(string location, CancellationToken cancellationToken = default);
}
