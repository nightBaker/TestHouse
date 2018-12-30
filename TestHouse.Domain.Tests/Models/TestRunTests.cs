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
            var project = new ProjectAggregate("name", "description");
            var suit = new Suit("name", "description",0);
            var testCase = new TestCase("name", "description", "expectedResult", suit,0);
            var testRun = new TestRun("name", "description",project, new List<TestRunCase>());
            

            Assert.NotNull(suit);
            Assert.NotNull(project);
            Assert.NotNull(testCase);
            Assert.NotNull(testRun);

            Assert.Throws<ArgumentException>(() => new TestRun("", "descr",project, new List<TestRunCase>()));
            Assert.Throws<ArgumentException>(() => new TestRun("name", "descr", null, new List<TestRunCase>()));
        }
    }
}
