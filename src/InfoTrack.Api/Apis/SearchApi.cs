using Microsoft.Extensions.Logging;
using InfoTrack.Core.Models;
using InfoTrack.Core.Catalogue;


namespace InfoTrack.Api.Api;

public class SearchApi : ISearchApi
{
    private readonly ILogger<SearchApi> _logger;
    private readonly ISearchCatalogue _catalogue;

    public SearchApi(
        ISearchCatalogue searchCatalogue,
        ILogger<SearchApi> logger)
    {
        _catalogue = searchCatalogue;
        _logger = logger;
    }

    public async Task<List<Solicitor>> SearchAsync(IEnumerable<string> locations)
    {
        return await _catalogue.SearchAsync(locations);
    }
}
