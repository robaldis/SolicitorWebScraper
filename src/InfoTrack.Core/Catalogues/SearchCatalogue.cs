using Microsoft.Extensions.Logging;
using InfoTrack.Models;
using InfoTrack.Parsing;
using InfoTrack.Scraping;


namespace InfoTrack.Core.Catalogue;

public class SearchCatalogue : ISearchCatalogue
{
    private readonly IWebScraper _scrapper;
    private readonly ISolicitorParser _parser;
    private readonly ILogger<SearchCatalogue> _logger;

    public SearchCatalogue(
        IWebScraper scrapper,
        ISolicitorParser parser,
        ILogger<SearchCatalogue> logger
        )
    {
        _scrapper = scrapper;
        _parser = parser;
        _logger = logger;
    }

    public async Task<List<Solicitor>> SearchAsync(IEnumerable<string> locations)
    {
        var results = new List<Solicitor>();

        foreach (var location in locations)
        {
            try
            {
                _logger.LogInformation("Scraping location: {Location}", location);
                var html = await _scrapper.GetHtmlAsync(location);
                var solicitors = _parser.Parse(html, location);
                results.AddRange(solicitors);
                _logger.LogInformation("Found {Count} solicitors in {Location}", solicitors.Count, location);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to scrape location: {Location}", location);
            }
        }

        return [.. results.OrderBy(s => s.Location).ThenBy(s => s.Name)];
    }
}
