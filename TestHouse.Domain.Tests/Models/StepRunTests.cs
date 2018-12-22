using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class StepRunTests
    {
        [Fact]
        public void Creation()
        {
            var step = new Step(1, "description", "result");
            var stepRun = new StepRun(step);
            Assert.NotNull(step);
            Assert.NotNull(stepRun);

            var except = false;
            try
            {
                var incorect = new StepRun(null);
            }
            catch (ArgumentException)
            {
                except = true;
            }

            Assert.True(except);
        }
    }
}
