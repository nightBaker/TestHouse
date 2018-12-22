using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class TestRunTests
    {
        [Fact]
        public void Creation()
        {
            var project = new Project("name", "description");
            var suit = new Suit("name", "description", project);
            var testCase = new TestCase("name", "description", "expectedResult", suit);
            var testRun = new TestRun("name", "description", new List<TestCaseRun>());
            

            Assert.NotNull(suit);
            Assert.NotNull(project);
            Assert.NotNull(testCase);
            Assert.NotNull(testRun);

            var except = false;
            try
            {
                var incorect = new TestRun("","descr", new List<TestCaseRun>());
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);
            
        }
    }
}
