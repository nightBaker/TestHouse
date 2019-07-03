using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestHouse.DTOs.DTOs;
using TestHouse.DTOs.Models;

namespace TestHouse.Web.Blazor.Services
{
    public class TestCasesService
    {
        private HttpClient _httpClient;

        public TestCasesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TestCaseDto> Add(TestCaseModel model)
        {
            var content = new StringContent(Json.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5000/api/testCase", content);
            var result = await response.Content.ReadAsStringAsync();
            return Json.Deserialize<TestCaseDto>(result);
        }
    }
}
