using InfoTrack.Core.Models;

namespace InfoTrack.Api.Api;

public interface ISearchApi
{
    Task<List<Solicitor>> SearchAsync(IEnumerable<string> locations);
}
