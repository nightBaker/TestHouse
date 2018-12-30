using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Project - parent of all suites
    /// </summary>
    public class ProjectAggregate
    {
        private List<TestRun> _testRuns;
        private List<Suit> _suits;

        /// <summary>
        /// Project id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Project description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Project creation date
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Root suit 
        /// </summary>
        public Suit RootSuit { get; private set; }

        /// <summary>
        /// Test runs
        /// </summary>
        public IReadOnlyCollection<TestRun> TestRuns => _testRuns;

        /// <summary>
        /// Project suits
        /// </summary>
        public IReadOnlyCollection<Suit> Suits => _suits;

        public ProjectAggregate(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name is not specified", "name");

            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;

            RootSuit = new Suit("root", "root", 0);
            _testRuns = new List<TestRun>();
            _suits = new List<Suit>();
        }

        /// <summary>
        /// Add new suit to the project
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="order"></param>
        /// <param name="parentSuitId"></param>
        public Suit AddSuit(string name, string description, long? parentSuitId = null)
        {
            var parentSuit = parentSuitId.HasValue
                            ? _suits.FirstOrDefault(s => s.Id == parentSuitId)
                            : RootSuit
                            ?? throw new ArgumentException("Project does not have suit with specified parentSuitId", nameof(parentSuitId));
            var order = _suits.Where(s => s.Id == parentSuit.Id).Max(s => s.Order) + 1;
            var suit = new Suit(name, description, order, parentSuit);
            _suits.Add(suit);

            return suit;
        }
    }
}
