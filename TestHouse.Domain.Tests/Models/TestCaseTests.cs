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
            var tcName = "name";
            var tcDescription = "description";
            var tcExpectedResult = "expectedResult";
            int tcOrder = 1;

            var project = new ProjectAggregate("name", "description");
            var suit = new Suit("name", "description",0);
            var testCase = new TestCase(tcName, tcDescription, tcExpectedResult, suit, tcOrder);

            Assert.NotNull(suit);
            Assert.NotNull(project);
            Assert.NotNull(testCase);

            Assert.Equal(testCase.Name, tcName);
            Assert.Equal(testCase.Description, tcDescription);
            Assert.Equal(testCase.ExpectedResult, tcExpectedResult);
            Assert.Equal(testCase.Order, tcOrder);
            Assert.Equal(testCase.Suit, suit);

            Assert.Throws<ArgumentException>(() => new TestCase("", "descr", "result", suit, 0));            
            Assert.Throws<ArgumentException>(() => new TestCase("name", "descr", "result", null, 0));  
            

        }
    }
}
