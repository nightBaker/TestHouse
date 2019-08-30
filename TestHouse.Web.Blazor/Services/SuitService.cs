using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestHouse.DTOs.DTOs;
using TestHouse.DTOs.Models;

namespace TestHouse.Web.Blazor.Services
{
    public class SuitService
    {
        private HttpClient _httpClient;

        public SuitService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SuitDto> Add(SuitModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5000/api/suit", content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SuitDto>(result);
        }
    }
}
