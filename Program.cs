using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;

namespace GetNuGetVer
{
    class VersionsResponse
    {
        [DataMember(Name = "versions")]
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

            // default serializer change to allow private/exclude null/snake_case serializer.
            JsonSerializer.SetDefaultResolver(StandardResolver.AllowPrivateExcludeNullSnakeCase);
            var versionsResponse = JsonSerializer.Deserialize<VersionsResponse>(versionsResponseBytes);

            var lastVersion = versionsResponse.Versions[^1]; //(length-1)
            // and so on ..
         }
    }
}