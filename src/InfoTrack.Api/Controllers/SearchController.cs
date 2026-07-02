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

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { message = "InfoTrack API is alive", timestamp = DateTime.UtcNow });
    }

    [HttpGet("locations")]
    public IActionResult GetLocations()
    {
        return Ok(_api.GetLocations());
    }

    [HttpGet("conveyancy")]
    public async Task<IActionResult> SearchConveyancy([FromQuery] List<string> locations)
    {
        try
        {
            var results = await _api.SearchAsync(locations);
            return Ok(results);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
