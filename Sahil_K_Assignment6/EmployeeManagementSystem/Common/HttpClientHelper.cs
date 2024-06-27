using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Common
{
    public class HttpClientHelper
    {
        public static async Task<string> MakePostRequest(string baseUrl, string endpoint, string apiRequestData)
        {
            var socketHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            using (HttpClient httpClient = new HttpClient(socketHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);
                StringContent apiRequestContent = new StringContent(apiRequestData, Encoding.UTF8, "application/json");

                var httpResponse = await httpClient.PostAsync(endpoint, apiRequestContent);
                var httpResponseString = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    
                    throw new HttpRequestException($"Request failed with status code {httpResponse.StatusCode}: {httpResponseString}");
                }

                return httpResponseString;
            }
        }

        public static async Task<string> MakeGetRequest(string baseUrl, string endpoint)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            using (HttpClient httpClient = new HttpClient(socketsHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);

                var response = await httpClient.GetAsync(endpoint);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {responseString}");
                }

                return responseString;
            }
        }
    }
}
