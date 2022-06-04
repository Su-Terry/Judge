using System.Diagnostics;
using System.Net;

namespace Judge
{
    internal class Client
    {
        private const string version = "v2.0.0-alpha";
        const string versionURL = @"https://pastebin.com/raw/jBUYitzT";

        private static async Task<bool> CheckIsLatestVersion(string version)
        {
            using HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(versionURL);
            using HttpContent content = response.Content;
            string json = await content.ReadAsStringAsync();
            return json.Equals(version);
        }

        public static void Validate()
        {
            try
            {
                bool result = CheckIsLatestVersion(version).Result;
                if (!result)
                {
                    Console.WriteLine("The judge has been updated.");
                    Console.WriteLine("Plz re-download");
                }
            }
            catch
            {
                Console.WriteLine("OFFLINE MODE");
            }
        }
    }
}
