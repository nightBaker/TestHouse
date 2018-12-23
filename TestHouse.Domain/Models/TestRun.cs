using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.Domain.Models
{
    public class TestRun
    {
        public TestRun(string name, string description, Project project, List<TestCaseRun> testCases = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is not specified", "name");            

            Name = name;
            Description = description;
            TestCases = testCases ?? new List<TestCaseRun>();

            Project = project ?? throw new ArgumentException("Project is no specified", nameof(project));
        }

        /// <summary>
        /// Test run id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Test run name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Test run description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// List of test cases
        /// </summary>
        public List<TestCaseRun> TestCases { get; private set; }

        /// <summary>
        /// Project
        /// </summary>
        public Project Project { get; private set; }
    }
}
