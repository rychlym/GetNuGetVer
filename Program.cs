using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GetNuGetVer
{
    class VersionsResponse
    {
        public string[] Versions { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var packageName = "Enums.NET";
            var url = $"https://api.nuget.org/v3-flatcontainer/{packageName}/index.json";
            var httpClient = new HttpClient();           
            var response = await httpClient.GetAsync(url);
            var versionsResponseBytes = await response.Content.ReadAsByteArrayAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var versionsResponse = JsonSerializer.Deserialize<VersionsResponse>(versionsResponseBytes, options);

            var lastVersion = versionsResponse.Versions[^1]; //(length-1)
            // and so on ..
         }
    }
}