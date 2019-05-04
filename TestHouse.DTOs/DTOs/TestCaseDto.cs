using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;

namespace TestHouse.DTOs.DTOs
{
    public class TestCaseDto
    {
        public long Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public string ExpectedResult { get;  set; }
        public int Order { get;  set; }
        public List<Step> Steps { get;  set; }
    }    
}
