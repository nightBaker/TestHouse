using System;
using TestHouse.Domain.Enums;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Extended step for run
    /// </summary>
    public class StepRun
    {
        public StepRun(Step step)
        {
            Step = step ?? throw new ArgumentException("Step is not specified", "step");
            Status = StepRunStatus.None;
        }

        /// <summary>
        /// Step run id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Step
        /// </summary>
        public Step Step { get; private set; }

        /// <summary>
        /// Status
        /// </summary>
        public StepRunStatus Status { get; private set; }
    }
}