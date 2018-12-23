using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class StepTests
    {
        [Fact]
        public void Creation()
        {
            var step = new Step(1, "description", "result");
            Assert.NotNull(step);
            Assert.Throws<ArgumentException>(() => new Step(0, "", "result"));           
        }
    }
}
