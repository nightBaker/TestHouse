using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Services;
using TestHouse.Infrastructure.Repositories;
using TestHouse.Persistence;
using Xunit;

namespace TestHouse.Application.Tests
{
    public class SuitServiceTests
    {
        [Fact]
        public async Task AddSuitTest()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ProjectRespository>()
                    .UseSqlite(connection)
                    .Options;

                long projectId = 0;
                // Create the schema in the database
                using (var context = new ProjectRespository(options))
                {
                    context.Database.EnsureCreated();

                    var projectService = new ProjectService(context);
                    var project = await projectService.AddProject("test name", "test description");
                    projectId = project.Id;
                }

                // Run the test against one instance of the context                
                using (var repository = new ProjectRespository(options))
                {
                    var suitService = new SuitService(repository);
                    var suit = await suitService.AddSuitAsync("suit name", "suit description", projectId);

                    Assert.NotEqual(0, suit.Id);

                    var child = await suitService.AddSuitAsync("child name", "child description", projectId, suit.Id);

                    await Assert.ThrowsAsync<ArgumentException>(async () =>
                        await suitService.AddSuitAsync("child name", "child description", 5, suit.Id));
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProjectRespository(options))
                {
                    // 2 + root suit
                    Assert.Equal(3, context.Suits.Count());

                    var project = await context.GetAsync(projectId);
                    Assert.Collection(project.Suits, item =>
                    {
                        Assert.Equal("suit name", item.Name);
                        Assert.Equal("suit description", item.Description);
                        Assert.NotNull(item.ParentSuit);
                        Assert.Equal(item.ParentSuit, project.RootSuit);
                    },
                    item =>
                    {
                        Assert.Equal("child name", item.Name);
                        Assert.Equal("child description", item.Description);
                        Assert.NotNull(item.ParentSuit);
                    });                                        
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
