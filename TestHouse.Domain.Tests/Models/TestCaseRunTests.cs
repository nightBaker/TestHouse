using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class TestCaseRunTests
    {
        [Fact]
        public void Creation()
        {
            var project = new ProjectAggregate("name", "description");
            var suit = new Suit("name", "description",0);
            var testCase = new TestCase("name", "description", "expectedResult", suit, 0);
            var testRun = new TestRun("name", "description" , new List<TestRunCase>());
            var steps = new List<StepRun>();
            var testCaseRun = new TestRunCase(testCase,steps);

            Assert.NotNull(suit);
            Assert.NotNull(project);
            Assert.NotNull(testCase);
            Assert.NotNull(testCaseRun);

            Assert.Equal(TestCaseStatus.None, testCaseRun.Status);
            Assert.NotNull(testCaseRun.Steps);
            Assert.Equal(steps, testCaseRun.Steps);

            Assert.Throws<ArgumentNullException>(() => new TestRunCase(null, null));
            Assert.Throws<ArgumentNullException>(() => new TestRunCase(testCase, null));           
        }
    }
}
