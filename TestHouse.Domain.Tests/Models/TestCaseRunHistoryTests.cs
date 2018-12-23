using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class TestCaseRunHistoryTests
    {
        [Fact]
        public void Creation()
        {
            var project = new Project("name", "description");
            var suit = new Suit("name", "description",project);
            var testCase = new TestCase("name", "description", "expectedResult", suit);
            var testRun = new TestRun("name", "description", new List<TestCaseRun>());
            var testCaseRun = new TestCaseRun(testCase, null, testRun);
            var testHistory = new TestCaseRunHistory(Enums.RunHistoryType.Status, "message", testCaseRun);

            Assert.NotNull(suit);
            Assert.NotNull(project);
            Assert.NotNull(testCase);
            Assert.NotNull(testCaseRun);
            Assert.NotNull(testRun);
            Assert.NotNull(testHistory);

            var except = false;
            try
            {
                var incorect = new TestCaseRunHistory(Enums.RunHistoryType.Status, "", testCaseRun);
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);


            except = false;
            try
            {
                var incorect = new TestCaseRunHistory(Enums.RunHistoryType.Status, "message", null);
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);
        }
    }
}
