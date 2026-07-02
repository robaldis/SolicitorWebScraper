# InfoTrack Conveyancing

## Components

### Front end

### Backend API


### Scrapper

### Parser

There are two shapes for the entries (top level, the rest), the parser needs to 
handle both


## Things to keep in mind

HttpClient usage

Never new HttpClient() per request — classic beginner mistake, causes socket exhaustion. Use IHttpClientFactory via services.AddHttpClient<HttpWebScraper>().
All scraper calls should be async all the way down (GetStringAsync, async Task<string>), no .Result or .Wait() blocking calls anywhere.

Models

Use record types for your DTOs (Solicitor, SearchRequest) — immutable, concise, shows familiarity with modern C#. Records are a decent flex for a company checking current .NET knowledge.

Async naming & exceptions

Suffix async methods with Async (ScrapeAsync, ParseAsync).
Don't let scraper failures for one location kill the whole batch — wrap per-location calls in try/catch, log, and continue, so if Bristol 404s the other 7 cities still return.
Avoid swallowing exceptions silently — at minimum log via ILogger<T>.

Add explicit CORS policy in Program.cs for your SPA's origin — this was called out as a common failure, so don't skip it or leave it as AllowAnyOrigin without at least knowing why.

Config

Put the base URL, location list defaults, and connection string in appsettings.json, not hardcoded — bind via IOptions<T> if you want to show that pattern too.


 

