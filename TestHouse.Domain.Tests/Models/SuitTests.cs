using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using Xunit;

namespace TestHouse.Domain.Tests.Models
{
    public class SuitTests
    {
        [Fact]
        public void Creation()
        {
            var project = new ProjectAggregate("name", "description");
            var suit = new Suit("name", "description",0);
            Assert.NotNull(suit);
            Assert.NotNull(project);

            Assert.Throws<ArgumentException>(() => new Suit("", "descr",0));
            Assert.Throws<ArgumentException>(() => new Suit("name", "descr",0, null));
        
        }
    }
}
