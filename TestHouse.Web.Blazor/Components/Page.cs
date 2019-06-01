using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestHouse.Web.Blazor.Components
{
    public enum PageId : byte
    {
        Projects,
        Cases,
        Runs,
        Reports
    }

    public struct Page
    {
        public string Description { get; set; }
        public string Uri { get; set; }
    }
}
