using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Toto.CineOrg.TestFramework
{
    public static class Extensions
    {
        public static HttpContent ToJsonContent(this object value)
        {
            var json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public static async Task<IList<TListItem>> ToListAsync<TListItem>(this HttpContent httpContent)
        {
            var json = await httpContent.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<TListItem>>(json);

            return content;
        }
        
        public static async Task<TObject> ToTypedObjectAsync<TObject>(this HttpContent httpContent)
        {
            var json = await httpContent.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<TObject>(json);

            return obj;
        }
        
        public static HttpResponseMessage EnsureStatusCode(this HttpResponseMessage response, HttpStatusCode statusCode)
        {
            if (response.StatusCode == statusCode) 
                return response;
            
            var message =
                $"Expected status code was '{statusCode}'. Actual status code is '{response.StatusCode}'.";
            throw new HttpRequestException(message);
        }

        public static string GetContentType(this HttpResponseMessage response)
        {
            return response.Content.Headers.ContentType.ToString();
        }
    }
}