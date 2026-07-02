using InfoTrack.Models;

namespace InfoTrack.Api.Api;

public interface ISearchApi
{
    List<string> GetLocations();
    Task<List<Solicitor>> SearchAsync(IEnumerable<string> locations);
}
