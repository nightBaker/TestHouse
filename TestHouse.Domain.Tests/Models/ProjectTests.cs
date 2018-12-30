using System;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class ProjectTests
    {
        [Fact]
        public void Creation()
        {
            var project = new ProjectAggregate("test name", "test description");

            Assert.NotNull(project);

            Assert.Throws<ArgumentException>(() => new ProjectAggregate(null, null));            
        }
    }
}
