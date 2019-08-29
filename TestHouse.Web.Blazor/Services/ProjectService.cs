using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Domain.Enums;
using TestHouse.DTOs.DTOs;
using TestHouse.DTOs.Models;

namespace TestHouse.Web.Blazor.Services
{
    public class ProjectService
    {
        private HttpClient _httpClient;

        public ProjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProjectAggregateDto> GetProject(long id)
        {
            Console.WriteLine("getting project");
            var project = await _httpClient.GetJsonAsync<ProjectAggregateDto>("http://localhost:5000/api/project/" + id);
            Console.WriteLine("got project");
            return project;
        }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            var projects = await _httpClient.GetJsonAsync<List<ProjectDto>> ("http://localhost:5000/api/project/all");
                    
            return projects.Where(x => x.State != ProjectAggregateState.Deleted).ToList();
        }

        public async Task<ProjectDto> AddProject(ProjectModel model)
        {            
            var response = await _httpClient.SendJsonAsync<ProjectDto>(HttpMethod.Post,  "http://localhost:5000/api/project", model);
            return response;
        }
        public async Task EditProject(long projectId, ProjectModel model)
        {            
            await _httpClient.SendJsonAsync(HttpMethod.Put, $"http://localhost:5000/api/project/{projectId}", model);
        }

        public async Task RemoveProject(long id)
        {
            await _httpClient.DeleteAsync("http://localhost:5000/api/project/" + id);
        }
    }
}
