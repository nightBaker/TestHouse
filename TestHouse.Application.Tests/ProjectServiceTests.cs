using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Services;
using TestHouse.Infrastructure.Repositories;
using Xunit;

namespace TestHouse.Application.Tests
{
    public class ProjectServiceTests
    {
        [Fact]
        public async Task AddProjectTest()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ProjectRespository>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ProjectRespository(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var repository = new ProjectRespository(options))
                {                    
                    var projectService = new ProjectService(repository);
                    var project = await projectService.AddProjectAsync("test name", "test description");

                    Assert.NotEqual(0, project.Id);
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProjectRespository(options))
                {
                    var project = await context.GetAsync(1);
                    Assert.Equal(1, context.Projects.Count());
                    Assert.Equal("test name", project.Name);
                    Assert.Equal("test description", project.Description);
                    Assert.NotEqual(0, project.Id);                    
                    Assert.NotNull(project.Suits);
                    Assert.NotNull(project.RootSuit);
                    Assert.NotNull(project.TestRuns);                    
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
