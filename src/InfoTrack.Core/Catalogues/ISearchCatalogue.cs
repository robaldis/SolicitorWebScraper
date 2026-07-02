using InfoTrack.Core.Models;

namespace InfoTrack.Core.Catalogue;

public interface ISearchCatalogue
{
    Task<List<Solicitor>> SearchAsync(IEnumerable<string> locations);
}
