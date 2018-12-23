using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class TestCaseRunTests
    {
        [Fact]
        public void Creation()
        {
            var project = new Project("name", "description");
            var suit = new Suit("name", "description", project);
            var testCase = new TestCase("name", "description", "expectedResult", suit);
            var testRun = new TestRun("name", "description", new List<TestCaseRun>());
            var testCaseRun = new TestCaseRun(testCase, null, testRun);

            Assert.NotNull(suit);
            Assert.NotNull(project);
            Assert.NotNull(testCase);
            Assert.NotNull(testCaseRun);

            Assert.Throws<ArgumentException>(() => new TestCaseRun(null, null, testRun));
            Assert.Throws<ArgumentException>(() => new TestCaseRun(testCase, null, null));           
        }
    }
}
