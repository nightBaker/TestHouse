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

            project.AddSuit("name", "description");

            Assert.NotNull(project);
            Assert.NotNull(project.RootSuit);
            Assert.NotNull(project.Suits);
            Assert.NotNull(project.TestRuns);

            Assert.Collection(project.Suits, item =>
            {
                Assert.Contains("name", item.Name);
                Assert.Contains("description", item.Description);
                Assert.NotNull(item.ParentSuit);
            });           
        }
    }
}
