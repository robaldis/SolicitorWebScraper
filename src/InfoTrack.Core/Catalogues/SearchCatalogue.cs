using Microsoft.Extensions.Logging;
using InfoTrack.Scraping;
using InfoTrack.Core.Models;


namespace InfoTrack.Core.Catalogue;

public class SearchCatalogue : ISearchCatalogue
{
    private readonly IWebScraper _scrapper;
    private readonly ILogger<SearchCatalogue> _logger;

    public SearchCatalogue(
        IWebScraper scrapper,
        ILogger<SearchCatalogue> logger
        )
    {
        _scrapper = scrapper;
        _logger = logger;
    }

    public async Task<List<Solicitor>> SearchAsync(IEnumerable<string> locations)
    {
        var results = new List<Solicitor>();

        foreach (var location in locations)
        {
            _logger.LogInformation(location);
            await _scrapper.GetHtmlAsync(location);
        }

        return results;
    }
}
