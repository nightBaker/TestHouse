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

            var except = false;
            try
            {
                var incorect = new TestCaseRun(null, null, testRun);
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);


            except = false;
            try
            {
                var incorect = new TestCaseRun(testCase, null, null);
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);
        }
    }
}
