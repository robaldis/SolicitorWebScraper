using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace InfoTrack.Scraping;


public class HttpWebScraper : IWebScraper
{
    private readonly HttpClient _httpClient;
    private readonly ScrapperOptions _options;
    private readonly ILogger<HttpWebScraper> _logger;

    public HttpWebScraper(
            HttpClient httpClient,
            IOptions<ScrapperOptions> options,
            ILogger<HttpWebScraper> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }
    public async Task<string> GetHtmlAsync(string location, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be empty", nameof(location));

        var sanitized = Uri.EscapeDataString(location.Trim().ToLowerInvariant());
        _logger.LogInformation($"Sanitized: {sanitized}");
        _logger.LogInformation($"BaseURL: {_options.BaseUrl}");
        var url = string.Format(_options.BaseUrl, sanitized);
        _logger.LogInformation($"URL: {url}");
        var html = await _httpClient.GetStringAsync(url, cancellationToken);

        // TEMP DEBUG - remove after inspecting
        await File.WriteAllTextAsync($"/tmp/{sanitized}.html", html, cancellationToken);
        return html;
    }
}
