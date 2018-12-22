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
            var except = false;
            try
            {
                var incorect = new Project(null, null);
            }
            catch (ArgumentException)
            {
                
                except = true;
            }

            Assert.True(except);
        }
    }
}
