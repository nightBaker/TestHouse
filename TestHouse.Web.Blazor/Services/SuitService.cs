using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestHouse.DTOs.DTOs;
using TestHouse.DTOs.Models;
using Microsoft.AspNetCore.Components;

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
            var response = await _httpClient.SendJsonAsync<SuitDto>(HttpMethod.Post, "http://localhost:5000/api/suit", model);
            return response;
        }
    }
}
