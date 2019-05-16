using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
    public class TestCaseServiceTests
    {
        [Fact]
        public async Task AddTestCaseTest()
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
                long rootSuitId = 0;
                long suitId = 0;
                // Create the schema in the database
                using (var context = new ProjectRespository(options))
                {
                    context.Database.EnsureCreated();

                    var projectService = new ProjectService(context);
                    var project = await projectService.AddProjectAsync("test name", "test description");
                    projectId = project.Id;
                    rootSuitId = 1;

                    var suitService = new SuitService(context);
                    var suit = await suitService.AddSuitAsync("suit name", "suit description", projectId);
                    suitId = suit.Id;
                }

                // Run the test against one instance of the context                
                using (var repository = new ProjectRespository(options))
                {
                    var testCaseService = new TestCaseService(repository);
                    var testCase = await testCaseService.AddTestCaseAsync(
                        "name0", "description0", "expected0", projectId, rootSuitId,
                        new List<Step> { new Step(0, "description", "expectedResult") });

                    Assert.NotEqual(0, testCase.Id);
                    Assert.Collection(testCase.Steps, item =>
                    {
                        Assert.Equal("description", item.Description);
                        Assert.Equal("expectedResult", item.ExpectedResult);                        
                        Assert.Equal(0, item.Order);
                        Assert.NotEqual(0, item.Id);
                    });

                    var testCase1 = await testCaseService.AddTestCaseAsync(
                        "name1", "description1", "expected1", projectId, rootSuitId, null);

                    Assert.Equal<int>(1, testCase1.Order);

                    await Assert.ThrowsAsync<ArgumentException>(async () =>
                        await testCaseService.AddTestCaseAsync(
                            "name0", "description0", "expected0", projectId + 1, rootSuitId,
                                new List<Step> { new Step(0, "description", "expectedResult") }));


                    var testCase2 = await testCaseService.AddTestCaseAsync(
                        "name2", "description2", "expected2", projectId, suitId, new List<Step> { new Step(1, "description", "expectedResult") });

                    var testCase3 = await testCaseService.AddTestCaseAsync(
                        "name3", "description3", "expected3", projectId, suitId, null);

                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProjectRespository(options))
                {
                    // 2 + root suit
                    Assert.Equal(4, context.TestCases.Count());
                    Assert.Equal(2, context.Suits.Count());
                    Assert.Equal(1, context.Projects.Count());
                    Assert.Equal(2, context.Steps.Count());

                    var project = await context.GetAsync(projectId);
                    Assert.Collection(project.RootSuit.TestCases, item =>
                    {
                        Assert.Equal("name0", item.Name);
                        Assert.Equal("description0", item.Description);
                        Assert.NotNull(item.Steps);
                        Assert.NotEmpty(item.Steps);
                        Assert.Equal<int>(0,item.Order);

                        Assert.Collection(item.Steps, step =>
                        {
                            Assert.Equal("description", step.Description);
                            Assert.Equal("expectedResult", step.ExpectedResult);
                            Assert.Equal(0, step.Order);                            
                        });
                    },
                    item =>
                    {
                        Assert.Equal("name1", item.Name);
                        Assert.Equal("description1", item.Description);
                        Assert.NotNull(item.Steps);                        
                        Assert.Equal<int>(1, item.Order);
                    });


                    Assert.Collection(project.Suits[0].TestCases, item =>
                    {
                        Assert.Equal("name2", item.Name);
                        Assert.Equal("description2", item.Description);
                        Assert.NotNull(item.Steps);
                        Assert.NotEmpty(item.Steps);
                        Assert.Equal<int>(0, item.Order);

                        Assert.Collection(item.Steps, step =>
                        {
                            Assert.Equal("description", step.Description);
                            Assert.Equal("expectedResult", step.ExpectedResult);
                            Assert.Equal(1, step.Order);
                        });
                    },
                    item =>
                    {
                        Assert.Equal("name3", item.Name);
                        Assert.Equal("description3", item.Description);
                        Assert.NotNull(item.Steps);
                        Assert.Equal<int>(1, item.Order);
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
