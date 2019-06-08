using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Infastructure.Repositories
{
    public interface IProjectRepository
    {
        Task<ProjectAggregate> GetAsync(long id);
        Task RemoveAsync(long id);
        Task<ProjectAggregate> GetAsync(long id, long testRunId);
        Task SaveAsync();
        void Add(ProjectAggregate project);
        Task<List<ProjectAggregate>> GetAllAsync();
    }
}
