using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Extensions
{
    

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
