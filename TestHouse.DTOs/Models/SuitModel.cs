using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestHouse.DTOs.Models
{
    public class SuitModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public long ProjectId { get; set; }

        public long ParentId { get; set; }
    }
}
