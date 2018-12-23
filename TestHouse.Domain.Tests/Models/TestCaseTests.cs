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

            Assert.Throws<ArgumentException>(() => new TestCase("", "descr", "result", suit));
            Assert.Throws<ArgumentException>(() => new TestCase("name", "descr", "", suit));
            Assert.Throws<ArgumentException>(() => new TestCase("name", "descr", "result", null));            
        }
    }
}
