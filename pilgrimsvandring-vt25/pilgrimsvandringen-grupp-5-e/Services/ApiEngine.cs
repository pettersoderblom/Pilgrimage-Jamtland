using System.Text.Json;
using System.Text;

namespace pilgrimsvandringen_grupp_5_e.Services
{
    // got the code from Eriks lesson
    public class ApiEngine
    {        
        private readonly HttpClient _httpClient;

        public ApiEngine(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> GetAsync<TResponse>(string url, string? apiKey = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(apiKey))
            {
                request.Headers.Add("x-api-key", apiKey);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // POST-request (API-nyckel i body)
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, string apiKey)
        {
            var requestBody = new { apiKey, data = request };
            return await SendRequestAsync<TResponse>(url, HttpMethod.Post, requestBody);
        }

        //  PUT-request (API-nyckel i body)
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest request, string apiKey)
        {
            var requestBody = new { apiKey, data = request };
            return await SendRequestAsync<TResponse>(url, HttpMethod.Put, requestBody);
        }

        // DELETE-request (API-nyckel i body)
        public async Task<TResponse> DeleteAsync<TRequest, TResponse>(string url, TRequest request, string apiKey)
        {
            var requestBody = new { apiKey, data = request };
            return await SendRequestAsync<TResponse>(url, HttpMethod.Delete, requestBody);
        }

        // Send
        private async Task<TResponse> SendRequestAsync<TResponse>(string url, HttpMethod method, object requestBody)
        {
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(method, url) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
