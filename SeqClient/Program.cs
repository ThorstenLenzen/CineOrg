using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Toto.CineOrg.SeqClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string seqApiKey = "PpEgnQlF8z5BrMFHHg6C";
            var baseAddress = new Uri("http://localhost:5341/");


            var client = new HttpClient
            {
                BaseAddress = baseAddress
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Seq-ApiKey", seqApiKey);

            var response = await client.GetStringAsync("api");
        }
    }
}