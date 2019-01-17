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

        [Fact]
        public async Task GetAllTest()
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
                    await projectService.AddProjectAsync("test name 1", "test description 1");
                    await projectService.AddProjectAsync("test name 2", "test description 2");


                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProjectRespository(options))
                {
                    var projectService = new ProjectService(context);
                    var projects = await projectService.GetAllAsync();

                    Assert.Collection(projects, project =>
                    {
                        Assert.Equal(1, project.Id);
                        Assert.Equal("test name 1", project.Name);
                        Assert.Equal("test description 1", project.Description);
                    },
                    project =>
                    {
                        Assert.Equal(2, project.Id);
                        Assert.Equal("test name 2", project.Name);
                        Assert.Equal("test description 2", project.Description);
                    });
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetTest()
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
                    await projectService.AddProjectAsync("test name 1", "test description 1");

                    var suitService = new SuitService(repository);

                    await suitService.AddSuitAsync("suit level 1", "suit description 1", 1, 1);
                    await suitService.AddSuitAsync("suit level 2", "suit description 2", 1, 2);
                    await suitService.AddSuitAsync("suit level 1.1", "suit description 1.1", 1, 1);
                    await suitService.AddSuitAsync("suit level 3", "suit description 3", 1, 3);

                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProjectRespository(options))
                {
                    var projectService = new ProjectService(context);
                    var project = await projectService.GetAsync(1);

                    Assert.Equal(1, project.RootSuit.Id);
                    Assert.Equal("root", project.RootSuit.Name);
                    Assert.Equal("root", project.RootSuit.Description);

                    Assert.Collection(project.RootSuit.Suits, suit =>
                    {
                        Assert.Equal(2, suit.Id);
                        Assert.Equal("suit level 1", suit.Name);
                        Assert.Equal("suit description 1", suit.Description);

                        Assert.Collection(suit.Suits, suit1 =>
                        {
                            Assert.Equal(3, suit1.Id);
                            Assert.Equal("suit level 2", suit1.Name);
                            Assert.Equal("suit description 2", suit1.Description);

                            Assert.Collection(suit1.Suits, suit2 =>
                            {
                                Assert.Equal(5, suit2.Id);
                                Assert.Equal("suit level 3", suit2.Name);
                                Assert.Equal("suit description 3", suit2.Description);

                                Assert.Null(suit2.Suits);
                            });
                        });
                    },
                    suit =>
                    {
                        Assert.Equal(4, suit.Id);
                        Assert.Equal("suit level 1.1", suit.Name);
                        Assert.Equal("suit description 1.1", suit.Description);

                        Assert.Null(suit.Suits);
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
