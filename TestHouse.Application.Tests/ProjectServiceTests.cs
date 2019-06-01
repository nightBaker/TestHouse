using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Services;
using TestHouse.Domain.Models;
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

                    var testCaseService = new TestCaseService(repository);
                    await testCaseService.AddTestCaseAsync("test case 1", "descr 1", "expected 1", 1, 1,
                                new List<Step> {
                                    new Step(0,"step descr 1", "expected 1"),
                                    new Step(1, "step descr 2", "expected 2")
                                });

                    await testCaseService.AddTestCaseAsync("test case 2", "descr 2", "expected 2", 1, 5,
                                new List<Step> {
                                    new Step(0,"step descr 3", "expected 3"),
                                    new Step(1, "step descr 4", "expected 4")
                                });

                    await testCaseService.AddTestCaseAsync("test case 3", "descr 3", "expected 3", 1, 5, null);

                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProjectRespository(options))
                {
                    var projectService = new ProjectService(context);
                    var project = await projectService.GetAsync(1);

                    Assert.Equal(1, project.RootSuit.Id);
                    Assert.Equal("root", project.RootSuit.Name);
                    Assert.Equal("root", project.RootSuit.Description);

                    Assert.Collection(project.RootSuit.TestCases, testCase =>
                    {
                        Assert.Equal("test case 1", testCase.Name);
                        Assert.Equal("descr 1", testCase.Description);
                        Assert.Equal("expected 1", testCase.ExpectedResult);

                        Assert.Collection(testCase.Steps, step =>
                        {
                            Assert.Equal("step descr 1", step.Description);
                            Assert.Equal("expected 1", step.ExpectedResult);
                            Assert.Equal(0, step.Order);
                        },
                        step=>
                        {
                            Assert.Equal("step descr 2", step.Description);
                            Assert.Equal("expected 2", step.ExpectedResult);
                            Assert.Equal(1, step.Order);
                        });
                    });

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

                                Assert.Collection(suit2.TestCases, testCase =>
                                {
                                    Assert.Equal("test case 2", testCase.Name);
                                    Assert.Equal("descr 2", testCase.Description);
                                    Assert.Equal("expected 2", testCase.ExpectedResult);

                                    Assert.Collection(testCase.Steps, step =>
                                    {
                                        Assert.Equal("step descr 3", step.Description);
                                        Assert.Equal("expected 3", step.ExpectedResult);
                                        Assert.Equal(0, step.Order);
                                    },
                                    step =>
                                    {
                                        Assert.Equal("step descr 4", step.Description);
                                        Assert.Equal("expected 4", step.ExpectedResult);
                                        Assert.Equal(1, step.Order);
                                    });
                                },
                                testCase =>
                                {
                                    Assert.Equal("test case 3", testCase.Name);
                                    Assert.Equal("descr 3", testCase.Description);
                                    Assert.Equal("expected 3", testCase.ExpectedResult);                                    
                                });
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

        [Theory]
        [InlineData("new name 1", "new description")]
        [InlineData("new name 2", "")]
        [InlineData("new name 3", " ")]
        [InlineData("new name 4", null)]
        public async Task UpdateInfoTest(string newName, string newDescription)
        {
            //ARRANGE
            var project = new ProjectAggregate("name", "description");

            var repo = new Mock<IProjectRepository>();
            repo.Setup(x => x.GetAsync(It.IsAny<long>())).ReturnsAsync(project).Verifiable();

            var projectService = new ProjectService(repo.Object);
            //ACT
            await projectService.UpdateProject(1, newName, newDescription);

            //ASSERT
            repo.Verify(x => x.SaveAsync());
            repo.Verify();

            Assert.Equal(newName, project.Name);
            Assert.Equal(newDescription, project.Description);
        }
    }
}
