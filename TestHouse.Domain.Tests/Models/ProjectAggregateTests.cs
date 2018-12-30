using System;
using System.Collections.Generic;
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

            Assert.NotNull(project);
            Assert.NotNull(project.RootSuit);
            Assert.NotNull(project.Suits);
            Assert.NotNull(project.TestRuns);

            

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
    }
}
