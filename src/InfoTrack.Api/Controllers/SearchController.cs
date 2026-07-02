using Microsoft.AspNetCore.Mvc;
using InfoTrack.Api.Api;

namespace InfoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{

    private readonly ISearchApi _api;

    public SearchController(ISearchApi searchApi)
    {
        _api = searchApi;
    }


    [HttpGet("ping")]
    public IActionResult Health()
    {
        return Ok(new { message = "InfoTrack API is alive", timestamp = DateTime.UtcNow });
    }

    [HttpGet("conveyancy")]
    public async Task<IActionResult> SearchConveyancy([FromQuery] List<string> locations)
    {
        Console.WriteLine(_api);
        await _api.SearchAsync(locations);
        return Ok(new {message = $"Trying to search, I will try do that... Found nothing"});
    }
}
