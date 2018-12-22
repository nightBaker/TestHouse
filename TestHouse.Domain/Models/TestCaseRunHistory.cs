using System;
using TestHouse.Domain.Enums;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Change history of test case run
    /// </summary>
    public class TestCaseRunHistory
    {        
        /// <summary>
        /// test case run history id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Type of history 
        /// </summary>
        public RunHistoryType Type { get; private set; }

        /// <summary>
        /// History message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Parent test case run
        /// </summary>
        public TestCaseRun TestCaseRun { get; private set; }


        public TestCaseRunHistory(RunHistoryType type, string message, TestCaseRun testCaseRun)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Message is not specified", "message");

            Type = type;
            Message = message;
            CreatedAt = DateTime.UtcNow;
            TestCaseRun = testCaseRun ?? throw new ArgumentException("Test case run is not specified", nameof(testCaseRun));
        }
    }
}