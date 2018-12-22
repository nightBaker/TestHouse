using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.Domain.Enums
{
    /// <summary>
    /// Test case status
    /// </summary>
    public enum TestCaseStatus
    {
        None,
        Started,
        Done,
        Blocked,
        Failed
    }
}
