using System;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests
{
    public class ProjectTests
    {
        [Fact]
        public void Creation()
        {
            var project = new Project("test name", "test description");

            Assert.NotNull(project);

            try
            {
                var incorect = new Project(null, null);
            }
            catch (ArgumentException)
            {
                Assert.True(true);
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
    }
}
