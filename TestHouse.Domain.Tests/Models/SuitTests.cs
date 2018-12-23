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
            var project = new Project("name", "description");
            var suit = new Suit("name", "description",project);
            Assert.NotNull(suit);
            Assert.NotNull(project);

            Assert.Throws<ArgumentException>(() => new Suit("", "descr", project));
            Assert.Throws<ArgumentException>(() => new Suit("name", "descr", null));
        
        }
    }
}
