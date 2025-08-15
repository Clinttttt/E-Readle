using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Infrastructure.Services
{
   public class BookServices
    {
        public async Task<string> DownloadBookTextAsync(string url)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            using var http = new HttpClient(handler);

            while (true)
            {
                if (url.StartsWith("http://"))
                    url = "https://" + url.Substring(7);

                var response = await http.GetAsync(url);

                // Follow redirects manually
                if ((int)response.StatusCode == 302 || (int)response.StatusCode == 301)
                {
                    url = response.Headers.Location!.OriginalString
                    ?? throw new Exception("Invalid redirect location");
                    if (!url.StartsWith("http"))
                        url = "https://" + url;
                    continue;
                }

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }





    }
}
