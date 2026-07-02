using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InfoTrack.Models;
using InfoTrack.Core.Catalogue;
using InfoTrack.Scraping;


namespace InfoTrack.Api.Api;

public class SearchApi : ISearchApi
{
    private readonly ISearchCatalogue _catalogue;
    private readonly ScrapperOptions _options;
    private readonly ILogger<SearchApi> _logger;

    public SearchApi(
        ISearchCatalogue searchCatalogue,
        IOptions<ScrapperOptions> options,
        ILogger<SearchApi> logger)
    {
        _catalogue = searchCatalogue;
        _options = options.Value;
        _logger = logger;
    }

    public List<string> GetLocations() => _options.AllowedLocations;

    public async Task<List<Solicitor>> SearchAsync(IEnumerable<string> locations)
    {
        var invalid = locations
            .Where(l => !_options.AllowedLocations.Contains(l, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (invalid.Count > 0)
        {
            _logger.LogWarning("Invalid locations requested: {Locations}", invalid);
            throw new ArgumentException(
                $"Invalid locations: {string.Join(", ", invalid)}. " +
                $"Allowed: {string.Join(", ", _options.AllowedLocations)}");
        }

        return await _catalogue.SearchAsync(locations);
    }
}
