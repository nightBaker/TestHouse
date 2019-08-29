using Microsoft.AspNetCore.Components;
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
    public class TestRunSerevice
    {
        private HttpClient _httpClient;

        public TestRunSerevice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<TestRunDto> AddTestRun(TestRunModel model)
        {            
            var response = await _httpClient.SendJsonAsync<TestRunDto>(HttpMethod.Post , "http://localhost:5000/api/TestRun", model);
            return response;
        }
    }
}
