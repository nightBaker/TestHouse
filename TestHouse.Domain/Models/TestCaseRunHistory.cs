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

        public RunHistoryType Type { get; set; }
    }
}