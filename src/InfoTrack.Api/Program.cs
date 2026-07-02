using InfoTrack.Api.Api;
using InfoTrack.Core.Catalogue;
using InfoTrack.Scraping;

namespace InfoTrack.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<ISearchApi, SearchApi>();
            builder.Services.AddScoped<ISearchCatalogue, SearchCatalogue>();

            builder.Services.Configure<ScrapperOptions>(
                builder.Configuration.GetSection(ScrapperOptions.SectionName));

            builder.Services.AddHttpClient<IWebScraper, HttpWebScraper>(client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; InfoTrackBot/1.0)");
                client.Timeout = TimeSpan.FromSeconds(15);
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
