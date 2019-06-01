using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TestHouse.Domain.Models;

namespace TestHouse.Domain.Tests.Models
{
    public class ProjectAggregateTests
    {
        [Theory]
        [InlineData("test1 name", "test description")]
        [InlineData("test2 name", "")]
        [InlineData("test3 name", null)]
        public void Creation(string name, string description)
        {
            var project = new ProjectAggregate(name,description);

            Assert.Equal(name, project.Name);
            Assert.Equal(description, project.Description);
            Assert.NotNull(project);            
            Assert.NotNull(project.Suits);
            Assert.Empty(project.Suits);
            Assert.NotNull(project.TestRuns);
            Assert.NotNull(project.RootSuit);
        
        }

        [Theory]
        [InlineData("","Description1")]
        [InlineData(" ", "Description2")]
        [InlineData(null, "Description3")]
        public void CreationExceptionTest(string name, string description)
        {
            Assert.Throws<ArgumentException>(() => new ProjectAggregate(name, description));
        }

        [Theory]
        [InlineData("suit1-1", "description1", "suit2-1", "description2")]
        [InlineData("suit1-2", "", "suit2-2", " ")]
        [InlineData("suit1-3", "", "suit2-3", null)]
        public void AddSuit(string suitName1 , string suitDescription1, string suitName2, string suitDescription2)
        {
            var project = new ProjectAggregate("name", "description");

            project.AddSuit(suitName1,suitDescription1);
            project.AddSuit(suitName2, suitDescription2);
                        
            Assert.NotNull(project.Suits);
            Assert.NotEmpty(project.Suits);

            Assert.Collection(project.Suits,            
                item =>
            {
                Assert.Equal(suitName1, item.Name);
                Assert.Equal(suitDescription1, item.Description);
                Assert.NotNull(item.ParentSuit);
                Assert.Equal<uint>(0,item.Order);
                Assert.Equal(item.ParentSuit, project.RootSuit);
            }, 
                item =>
            {
                Assert.Equal(suitName2, item.Name);
                Assert.Equal(suitDescription2, item.Description);
                Assert.NotNull(item.ParentSuit);
                Assert.Equal<uint>(1, item.Order);
                Assert.Equal(item.ParentSuit, project.RootSuit);
            });
                                    
        }


        [Theory]
        [InlineData("","description-1")]
        [InlineData(" ", "description-2")]
        [InlineData(null, "description-3")]
        public void AddSuitException(string name, string description)
        {
            var project = new ProjectAggregate("name", "description");

            Assert.Throws<ArgumentException>(() => project.AddSuit(name, description));
        }

        [Fact]
        public void AddTestCase()
        {
            var project = new ProjectAggregate("name", "description");

            project.AddSuit("order0", "description");
            project.AddSuit("order1", "description1");

            var id = 0;
            foreach(var suit in project.Suits)
            {
                suit.Id = ++id;
            }

            var steps = new List<Step> { new Step(0, "descr", "expectedStep"), new Step(1, "descr", "expectedStep") };
            project.AddTestCase("testcase0", "descr0", "expected 0", 1, steps);
            project.AddTestCase("testcase1", "descr1", "expected 1", 1, steps);

            var suit0 = project.Suits.ToList()[0];
            var suit1 = project.Suits.ToList()[1];

            Assert.Collection(suit0.TestCases,
               item =>
               {
                   Assert.Contains("testcase0", item.Name);
                   Assert.Contains("descr0", item.Description);
                   Assert.NotNull(item.Steps);
                   Assert.NotEmpty(item.Steps);
                   Assert.Equal(2, item.Steps.Count);
                   Assert.NotNull(item.Suit);
                   Assert.Equal<int>(0, item.Order);                   
               },
               item =>
               {
                   Assert.Contains("testcase1", item.Name);
                   Assert.Contains("descr1", item.Description);
                   Assert.NotNull(item.Steps);
                   Assert.NotEmpty(item.Steps);
                   Assert.Equal(2, item.Steps.Count);
                   Assert.NotNull(item.Suit);
                   Assert.Equal<int>(1, item.Order);
               });
        }

        [Theory]
        [InlineData(0, "descr", "expectedStep", 1, "descr", "expectedStep")]
        public void AddStep(int stepOrder1, string stepDescription1, string stepExpectedResult1,
            int stepOrder2, string stepDescription2, string stepExpectedResult2)
        {
            var project = new ProjectAggregate("name", "description");

            project.AddSuit("order0", "description");

            var steps = new List<Step> {
                new Step(stepOrder1,stepDescription1,stepExpectedResult1),
                new Step(stepOrder2,stepDescription2,stepExpectedResult2)
            };
            project.AddTestCase("testcase0", "descr0", "expected 0", 0, steps);
            project.AddTestCase("testcase1", "descr1", "expected 1", 0, steps);

            Assert.Collection(project.RootSuit.TestCases,
                testCase=>
            {
                Assert.Collection(testCase.Steps,step=>
                {
                    Assert.Equal(stepOrder1, step.Order);
                    Assert.Equal(stepDescription1, step.Description);
                    Assert.Equal(stepExpectedResult1, step.ExpectedResult);
                },
                step =>
                {
                    Assert.Equal(stepOrder2, step.Order);
                    Assert.Equal(stepDescription2, step.Description);
                    Assert.Equal(stepExpectedResult2, step.ExpectedResult);
                });
            },
                testCase =>
                {
                    Assert.Collection(testCase.Steps,
                    step =>
                    {
                        Assert.Equal(stepOrder1, step.Order);
                        Assert.Equal(stepDescription1, step.Description);
                        Assert.Equal(stepExpectedResult1, step.ExpectedResult);
                    },
                    step =>
                    {
                        Assert.Equal(stepOrder2, step.Order);
                        Assert.Equal(stepDescription2, step.Description);
                        Assert.Equal(stepExpectedResult2, step.ExpectedResult);
                    });
                });
        }

        [Fact]
        public void AddRun()
        {
            var project = new ProjectAggregate("name", "description");

            project.AddSuit("order0", "description");
            project.AddSuit("order1", "description1");

            var id = 0;
            foreach (var suit in project.Suits)
            {
                suit.Id = ++id;
            }

            var steps = new List<Step> { new Step(0, "descr", "expectedStep"), new Step(1, "descr2", "expectedStep2") };
            project.AddTestCase("testcase0", "descr0", "expected 0", 1, steps);
            project.AddTestCase("testcase1", "descr1", "expected 1", 1, steps);

            var suit0 = project.Suits.ToList()[0];            
            foreach(var testCase in suit0.TestCases)
            {
                testCase.Id = ++id;
            }

            project.AddTestRun("first test run", "test run description", new HashSet<long> { 3, 4 });
            project.AddTestRun("second test run", "2 description", new HashSet<long>());

            Assert.Collection(project.TestRuns, testRun =>
            {
                Assert.Equal("first test run", testRun.Name);
                Assert.Equal("test run description", testRun.Description);
                Assert.NotNull(testRun.TestCases);
                Assert.NotEmpty(testRun.TestCases);
                Assert.Collection(testRun.TestCases, testCase =>
                {
                    Assert.NotNull(testCase.TestCase);
                    Assert.Equal(3, testCase.TestCase.Id);
                    Assert.NotNull(testCase.Steps);
                    Assert.NotEmpty(testCase.Steps);
                    Assert.Collection(testCase.Steps, step=>
                    {
                        Assert.NotNull(step.Step);
                        Assert.Equal(0, step.Step.Order);
                        Assert.Equal("descr", step.Step.Description);
                        Assert.Equal("expectedStep", step.Step.ExpectedResult);
                    },
                    step =>
                    {
                        Assert.NotNull(step.Step);
                        Assert.Equal(1, step.Step.Order);
                        Assert.Equal("descr2", step.Step.Description);
                        Assert.Equal("expectedStep2", step.Step.ExpectedResult);
                    });
                },
                 testCase =>
                 {
                     Assert.NotNull(testCase.TestCase);
                     Assert.Equal(4, testCase.TestCase.Id);
                     Assert.NotNull(testCase.Steps);
                     Assert.NotEmpty(testCase.Steps);
                     Assert.Collection(testCase.Steps, step =>
                     {
                         Assert.NotNull(step.Step);
                         Assert.Equal(0, step.Step.Order);
                         Assert.Equal("descr", step.Step.Description);
                         Assert.Equal("expectedStep", step.Step.ExpectedResult);
                     },
                    step =>
                    {
                        Assert.NotNull(step.Step);
                        Assert.Equal(1, step.Step.Order);
                        Assert.Equal("descr2", step.Step.Description);
                        Assert.Equal("expectedStep2", step.Step.ExpectedResult);
                    });
                 });
            },
            testRun =>
            {
                Assert.Equal("second test run", testRun.Name);
                Assert.Equal("2 description", testRun.Description);
                Assert.NotNull(testRun.TestCases);
                Assert.Empty(testRun.TestCases);
                
            });

            Assert.Throws<ArgumentNullException>(() => project.AddTestRun("name", "description", null));
        }

        [Fact]
        public void AddTestCasesToRun()
        {
            var project = new ProjectAggregate("name", "description");

            project.AddSuit("order0", "description");
            project.AddSuit("order1", "description1");

            var id = 0;
            foreach (var suit in project.Suits)
            {
                suit.Id = ++id;
            }

            var steps = new List<Step> { new Step(0, "descr", "expectedStep"), new Step(1, "descr2", "expectedStep2") };
            project.AddTestCase("testcase0", "descr0", "expected 0", 1, steps);
            project.AddTestCase("testcase1", "descr1", "expected 1", 1, steps);

            var suit0 = project.Suits.ToList()[0];
            foreach (var testCase in suit0.TestCases)
            {
                testCase.Id = ++id;
            }            
            project.AddTestRun("second test run", "2 description", new HashSet<long>());

            project.TestRuns[0].Id = 1;

            project.AddTestCasesToRun(new HashSet<long> { 3 }, 1);

            Assert.Collection(project.TestRuns, testRun =>
            {
                Assert.Collection(testRun.TestCases, testCase =>
                {
                    Assert.NotNull(testCase.TestCase);
                    Assert.Equal(3, testCase.TestCase.Id);
                    Assert.NotNull(testCase.Steps);
                    Assert.NotEmpty(testCase.Steps);
                    Assert.Collection(testCase.Steps, step =>
                    {
                        Assert.NotNull(step.Step);
                        Assert.Equal(0, step.Step.Order);
                        Assert.Equal("descr", step.Step.Description);
                        Assert.Equal("expectedStep", step.Step.ExpectedResult);
                    },
                    step =>
                    {
                        Assert.NotNull(step.Step);
                        Assert.Equal(1, step.Step.Order);
                        Assert.Equal("descr2", step.Step.Description);
                        Assert.Equal("expectedStep2", step.Step.ExpectedResult);
                    });
                });
            });

            project.AddTestCasesToRun(new HashSet<long> { 4 }, 1);

            Assert.Collection(project.TestRuns, testRun =>
            {               
                Assert.Collection(testRun.TestCases, testCase =>
                {
                    Assert.NotNull(testCase.TestCase);
                    Assert.Equal(3, testCase.TestCase.Id);
                    Assert.NotNull(testCase.Steps);
                    Assert.NotEmpty(testCase.Steps);
                    Assert.Collection(testCase.Steps, step =>
                    {
                        Assert.NotNull(step.Step);
                        Assert.Equal(0, step.Step.Order);
                        Assert.Equal("descr", step.Step.Description);
                        Assert.Equal("expectedStep", step.Step.ExpectedResult);
                    },
                    step =>
                    {
                        Assert.NotNull(step.Step);
                        Assert.Equal(1, step.Step.Order);
                        Assert.Equal("descr2", step.Step.Description);
                        Assert.Equal("expectedStep2", step.Step.ExpectedResult);
                    });
                },
                 testCase =>
                 {
                     Assert.NotNull(testCase.TestCase);
                     Assert.Equal(4, testCase.TestCase.Id);
                     Assert.NotNull(testCase.Steps);
                     Assert.NotEmpty(testCase.Steps);
                     Assert.Collection(testCase.Steps, step =>
                     {
                         Assert.NotNull(step.Step);
                         Assert.Equal(0, step.Step.Order);
                         Assert.Equal("descr", step.Step.Description);
                         Assert.Equal("expectedStep", step.Step.ExpectedResult);
                     },
                    step =>
                    {
                        Assert.NotNull(step.Step);
                        Assert.Equal(1, step.Step.Order);
                        Assert.Equal("descr2", step.Step.Description);
                        Assert.Equal("expectedStep2", step.Step.ExpectedResult);
                    });
                 });
            });
        }

        [Theory]
        [InlineData("Name1", "Description")]
        [InlineData("Name2", "")]
        [InlineData("Name3", null)]
        [InlineData("Name4", " ")]
        public void UpdateInfoTest(string name, string description)
        {
            var project = new ProjectAggregate("test name", "test description");

            project.UpdateInfo(name, description);

            Assert.Equal(name, project.Name);
            Assert.Equal(description, project.Description);
        }


        [Theory]
        [InlineData("", "Description1")]
        [InlineData(" ", "Description2")]
        [InlineData(null, "Description3")]
        public void UpdateInfoExceptionTest(string name, string description)
        {
            var project = new ProjectAggregate("test name", "test description");

            Assert.Throws<ArgumentException>(()=> project.UpdateInfo(name, description));
            
        }


    }
}
