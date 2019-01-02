using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class ProjectAggregateTests
    {
        [Fact]
        public void Creation()
        {
            var project = new ProjectAggregate("test name", "test description");

            Assert.NotNull(project);
            Assert.NotNull(project.RootSuit);
            Assert.NotNull(project.Suits);
            Assert.NotNull(project.TestRuns);

            Assert.Throws<ArgumentException>(() => new ProjectAggregate(null, null));
        }

        [Fact]
        public void AddSuit()
        {
            var project = new ProjectAggregate("name", "description");

            project.AddSuit("order0", "description");
            project.AddSuit("order1", "description1");
            
            Assert.NotNull(project.RootSuit);
            Assert.NotNull(project.Suits);            
            
            Assert.Collection(project.Suits, 
                item =>
            {
                Assert.Contains("order0", item.Name);
                Assert.Contains("description", item.Description);
                Assert.NotNull(item.ParentSuit);
                Assert.Equal<uint>(0,item.Order);
                Assert.Equal(item.ParentSuit, project.RootSuit);
            }, 
                item =>
            {
                Assert.Contains("order1", item.Name);
                Assert.Contains("description1", item.Description);
                Assert.NotNull(item.ParentSuit);
                Assert.Equal<uint>(1, item.Order);
                Assert.Equal(item.ParentSuit, project.RootSuit);
            });
                        

            Assert.Throws<ArgumentException>(() => project.AddSuit("", "descr"));
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
                suit.Id = id++;
            }

            var steps = new List<Step> { new Step(0, "descr", "expectedStep"), new Step(1, "descr", "expectedStep") };
            project.AddTestCase("testcase0", "descr0", "expected 0", 0, steps);
            project.AddTestCase("testcase1", "descr1", "expected 1", 0, steps);

            var suit0 = project.Suits.ToList()[0];
            var suit1 = project.Suits.ToList()[1];

            Assert.Collection(suit0.TestCases,
               item =>
               {
                   Assert.Contains("testcase0", item.Name);
                   Assert.Contains("descr0", item.Description);
                   Assert.NotNull(item.Steps);
                   Assert.NotNull(item.Suit);
                   Assert.Equal<uint>(0, item.Order);                   
               },
               item =>
               {
                   Assert.Contains("testcase1", item.Name);
                   Assert.Contains("descr1", item.Description);
                   Assert.NotNull(item.Steps);
                   Assert.NotNull(item.Suit);
                   Assert.Equal<uint>(1, item.Order);
               });
        }

        [Fact]
        public void AddStep()
        {
            var project = new ProjectAggregate("name", "description");

            project.AddSuit("order0", "description");

            var steps = new List<Step> { new Step(0, "descr", "expectedStep"), new Step(1, "descr", "expectedStep") };
            project.AddTestCase("testcase0", "descr0", "expected 0", 0, steps);
            project.AddTestCase("testcase1", "descr1", "expected 1", 0, steps);
        }
    }
}
