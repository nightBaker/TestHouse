using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Domain.Models;

namespace TestHouse.Infrastructure.Repositories
{
    public class ProjectRespository : DbContext,  IProjectRepository
    {
        public ProjectRespository(DbContextOptions<ProjectRespository> options)
            : base(options)
        {

        }

        public DbSet<ProjectAggregate> Projects { get; set; }

        public DbSet<Suit> Suits { get; set; }

        public DbSet<TestCase> TestCases { get; set; }

        public DbSet<TestRun> TestRuns { get; set; }

        public DbSet<TestRunCase> TestRunCases { get; set; }

        public void Add(ProjectAggregate project)
        {
            Projects.Add(project);
        }

        public async Task<ProjectAggregate> GetAsync(long id)
        {
            return await Projects.Include(p=>p.RootSuit)
                                .Include(project => project.Suits)
                                    .ThenInclude(suit => suit.TestCases)
                                .Include(project => project.TestRuns)
                                    .ThenInclude(testRun => testRun.TestCases)
                                .Where(p => p.Id == id)
                                .SingleAsync();
        }

        public Task SaveAsync()
        {
            return this.SaveChangesAsync();
        }
    }
}
