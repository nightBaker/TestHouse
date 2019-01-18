using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Models
{
    public class SuitDto
    {
        public long Id { get; set; }

        public IEnumerable<SuitDto> Suits { get; set; }
        public IEnumerable<TestCaseDto> TestCases { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class SuitExtensions
    {
        public static IEnumerable<SuitDto> ToSuitsDto(this IEnumerable<Suit> suits)
        {
            foreach (var item in suits)
            {
                yield return item.ToSuitDto();
            }
        }

        public static SuitDto ToSuitDto(this Suit item)
        {
            return new SuitDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };
        }
    }
}
