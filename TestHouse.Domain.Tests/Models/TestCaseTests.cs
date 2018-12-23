using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class TestCaseTests
    {
        [Fact]
        public void Creation()
        {
            var project = new Project("name", "description");
            var suit = new Suit("name", "description",project);
            var testCase = new TestCase("name", "description", "expectedResult", suit);

            Assert.NotNull(suit);
            Assert.NotNull(project);
            Assert.NotNull(testCase);

            var except = false;
            try
            {
                var incorect = new TestCase("","descr","result",suit);
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);

            except = false;
            try
            {
                var incorect = new TestCase("name", "descr","", suit);
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);


            except = false;
            try
            {
                var incorect = new TestCase("name", "descr", "result", null);
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);
        }
    }
}
