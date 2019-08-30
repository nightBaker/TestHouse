using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Infastructure.Repositories
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Get project aggregate 
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Project aggregate</returns>
        Task<ProjectAggregate> GetAsync(long id);

        /// <summary>
        /// Get project with fully loaded test run (included test cases and so on)
        /// </summary>
        /// <param name="id">project id</param>
        /// <param name="testRunId">test run id</param>
        /// <returns>Project aggregate</returns>
        Task<ProjectAggregate> GetAsync(long id, long testRunId);

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        /// Add new project aggregate
        /// </summary>
        /// <param name="project"></param>
        void Add(ProjectAggregate project);

        /// <summary>
        /// Get all project aggregates 
        /// </summary>
        /// <returns>List of project aggregates</returns>
        Task<List<ProjectAggregate>> GetAllAsync();
    }
}
