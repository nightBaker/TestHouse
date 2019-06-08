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

        public DbSet<Step> Steps { get; set; }

        public DbSet<StepRun> TestRunSteps { get; set; }

        public void Add(ProjectAggregate project)
        {
            Projects.Add(project);
        }

        public async Task<List<ProjectAggregate>> GetAllAsync()
        {
            return await Projects.ToListAsync();
        }

        public async Task<ProjectAggregate> GetAsync(long id)
        {
            return await Projects.Include(p=>p.RootSuit)
                                    .ThenInclude(rootSuit => rootSuit.TestCases)
                                        .ThenInclude(testCase=>testCase.Steps)
                                .Include(project => project.Suits)
                                    .ThenInclude(suit => suit.TestCases)
                                        .ThenInclude(testCase=> testCase.Steps)
                                .Include(project => project.TestRuns)                                
                                .Where(p => p.Id == id)
                                .SingleOrDefaultAsync();
        }
        public async Task RemoveAsync(long id)
        {
            var removableProject = Projects.Where(p => p.Id == id).FirstOrDefault();
            Projects.Remove(removableProject);
            await SaveChangesAsync();
        }
        public async Task<ProjectAggregate> GetAsync(long id, long testRunId)
        {
            var project = await GetAsync(id);

            //load data for test run to context memory
            var run = TestRuns.Include(testRun => testRun.TestCases)
                                .ThenInclude(testRunCase => testRunCase.Steps)
                                   .ThenInclude(runStep => runStep.Step)
                            .Include(testRun => testRun.TestCases)
                                .ThenInclude(testRunCase => testRunCase.TestCase)
                            .Where(t => t.Id == testRunId)
                            .Single();
          
            return project;
        }

        public Task SaveAsync()
        {
            return this.SaveChangesAsync();
        }
    }
}
