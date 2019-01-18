using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Services;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;
using TestHouse.Infrastructure.Repositories;
using Xunit;

namespace TestHouse.Application.Tests
{
    public class TestRunServiceTests
    {
        [Fact]
        public async Task AddTestRunTest()
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

                    var testCaseService = new TestCaseService(context);

                    var testCase = await testCaseService.AddTestCaseAsync(
                        "name0", "description0", "expected0", projectId, rootSuitId,
                        new List<Step> { new Step(0, "description", "expectedResult") });
                    
                    var testCase1 = await testCaseService.AddTestCaseAsync(
                        "name1", "description1", "expected1", projectId, rootSuitId, null);
                                      
                    var testCase2 = await testCaseService.AddTestCaseAsync(
                        "name2", "description2", "expected2", projectId, suitId, new List<Step> { new Step(1, "description", "expectedResult") });

                    var testCase3 = await testCaseService.AddTestCaseAsync(
                        "name3", "description3", "expected3", projectId, suitId, null);
                }

                // Run the test against one instance of the context                
                using (var repository = new ProjectRespository(options))
                {
                    var testRunService = new TestRunService(repository);
                    var testRun = await testRunService.AddTestRunAsync(projectId, "first test run",
                                                "first description", new HashSet<long> { 2, 1 });

                    var testRun2 = await testRunService.AddTestRunAsync(projectId, "second test run",
                                                "second description", new HashSet<long> { 2, 1,4 });
                }

                // Use a separate instance of the context to verify correct data was saved to database
           
     using (var context = new ProjectRespository(options))
                {
                    // 2 + root suit                    
                    Assert.Equal(2, context.TestRuns.Count());
                    Assert.Equal(5, context.TestRunCases.Count());
                    Assert.Equal(2, context.TestRunSteps.Count());

                    var project = await context.GetAsync(projectId, 1);
                    await context.GetAsync(projectId, 2); // just for loading data into memory
                    Assert.Collection(project.TestRuns, item =>
                    {
                        Assert.Equal("first test run", item.Name);
                        Assert.Equal("first description", item.Description);
                        Assert.NotNull(item.TestCases);
                        Assert.NotEmpty(item.TestCases);
                        
                        Assert.Collection(item.TestCases, testCase =>
                        {
                            Assert.NotNull(testCase.TestCase);
                            Assert.Equal(1, testCase.TestCase.Id);
                            Assert.Equal(TestCaseStatus.None, testCase.Status);
                            Assert.NotNull(testCase.Steps);
                            Assert.NotEmpty(testCase.Steps);

                            Assert.Collection(testCase.Steps, step =>
                            {
                                Assert.NotNull(step.Step);
                                Assert.Equal(StepRunStatus.None, step.Status);
                            });
                        },
                        testCase =>
                        {
                            Assert.NotNull(testCase.TestCase);
                            Assert.Equal(2, testCase.TestCase.Id);
                            Assert.Equal(TestCaseStatus.None, testCase.Status);
                            Assert.NotNull(testCase.Steps);
                            Assert.Empty(testCase.Steps);                            
                        });
                    }, 
                    item =>
                    {
                        Assert.Equal("second test run", item.Name);
                        Assert.Equal("second description", item.Description);
                        Assert.NotNull(item.TestCases);
                        Assert.NotEmpty(item.TestCases);

                        Assert.Collection(item.TestCases, testCase =>
                        {
                            Assert.NotNull(testCase.TestCase);
                            Assert.Equal(1, testCase.TestCase.Id);
                            Assert.Equal(TestCaseStatus.None, testCase.Status);
                            Assert.NotNull(testCase.Steps);
                            Assert.NotEmpty(testCase.Steps);

                            Assert.Collection(testCase.Steps, step =>
                            {
                                Assert.NotNull(step.Step);
                                Assert.Equal(StepRunStatus.None, step.Status);
                            });
                        },
                        testCase =>
                        {
                            Assert.NotNull(testCase.TestCase);
                            Assert.Equal(2, testCase.TestCase.Id);
                            Assert.Equal(TestCaseStatus.None, testCase.Status);
                            Assert.NotNull(testCase.Steps);
                            Assert.Empty(testCase.Steps);
                        },
                        testCase =>
                        {
                            Assert.NotNull(testCase.TestCase);
                            Assert.Equal(4, testCase.TestCase.Id);
                            Assert.Equal(TestCaseStatus.None, testCase.Status);
                            Assert.NotNull(testCase.Steps);
                            Assert.Empty(testCase.Steps);
                        });
                    });                   
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task AddCasesToRunTest()
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
                long testRunId = 0;
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

                    var testCaseService = new TestCaseService(context);

                    var testCase = await testCaseService.AddTestCaseAsync(
                        "name0", "description0", "expected0", projectId, rootSuitId,
                        new List<Step> { new Step(0, "description", "expectedResult") });

                    var testCase1 = await testCaseService.AddTestCaseAsync(
                        "name1", "description1", "expected1", projectId, rootSuitId, null);

                    var testCase2 = await testCaseService.AddTestCaseAsync(
                        "name2", "description2", "expected2", projectId, suitId, new List<Step> { new Step(1, "description", "expectedResult") });

                    var testCase3 = await testCaseService.AddTestCaseAsync(
                        "name3", "description3", "expected3", projectId, suitId, null);

                    var testRunService = new TestRunService(context);
                    var testRun = await testRunService.AddTestRunAsync(projectId, "first test run",
                                                "first description", new HashSet<long> { 2 });

                    await testRunService.AddTestRunAsync(projectId, "second test run",
                                                "second description", new HashSet<long> { 2 , 1  });

                    testRunId = testRun.Id;
                }

                // Run the test against one instance of the context                
                using (var repository = new ProjectRespository(options))
                {
                    var testRunService = new TestRunService(repository);
                    await testRunService.AddTestCases(projectId, testRunId, new HashSet<long> { 2, 4, 1 });
                }

                // Use a separate instance of the context to verify correct data was saved to database

                using (var context = new ProjectRespository(options))
                {
                    // 2 + root suit                    
                    Assert.Equal(2, context.TestRuns.Count());
                    Assert.Equal(5, context.TestRunCases.Count());
                    Assert.Equal(2, context.TestRunSteps.Count());
                    Assert.Equal(2, context.TestRuns.Count());

                    var project = await context.GetAsync(projectId, testRunId);
                    Assert.Collection(project.TestRuns, item =>
                    {                        
                        Assert.NotNull(item.TestCases);
                        Assert.NotEmpty(item.TestCases);

                        Assert.Collection(item.TestCases, 
                        testCase =>
                        {
                            Assert.NotNull(testCase.TestCase);
                            Assert.Equal(2, testCase.TestCase.Id);
                            Assert.Equal(TestCaseStatus.None, testCase.Status);
                            Assert.NotNull(testCase.Steps);
                            Assert.Empty(testCase.Steps);
                        },
                        testCase =>
                        {
                            Assert.NotNull(testCase.TestCase);
                            Assert.Equal(1, testCase.TestCase.Id);
                            Assert.Equal(TestCaseStatus.None, testCase.Status);
                            Assert.NotNull(testCase.Steps);
                            Assert.NotEmpty(testCase.Steps);

                            Assert.Collection(testCase.Steps, step =>
                            {
                                Assert.NotNull(step.Step);
                                Assert.Equal(StepRunStatus.None, step.Status);
                            });
                        },
                        testCase =>
                        {
                            Assert.NotNull(testCase.TestCase);
                            Assert.Equal(4, testCase.TestCase.Id);
                            Assert.Equal(TestCaseStatus.None, testCase.Status);
                            Assert.NotNull(testCase.Steps);
                            Assert.Empty(testCase.Steps);
                        });
                    }, testRun => { });
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
