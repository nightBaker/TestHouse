using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;

namespace TestHouse.DTOs.DTOs
{
    public class StepRunDto
    {
        /// <summary>
        /// Step run id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Step
        /// </summary>
        public Step Step { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public StepRunStatus Status { get; set; }
    }
}
