using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.DTOs.DTOs
{
    public class SuitDto
    {
        public long Id { get; set; }

        public IEnumerable<SuitDto> Suits { get; set; }
        public IEnumerable<TestCaseDto> TestCases { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
