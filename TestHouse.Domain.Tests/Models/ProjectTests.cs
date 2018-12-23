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
            var project = new Project("test name", "test description");

            Assert.NotNull(project);

            Assert.Throws<ArgumentException>(() => new Project(null, null));            
        }
    }
}
