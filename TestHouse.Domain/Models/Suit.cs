using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestHouse.Domain.Tests")]
namespace TestHouse.Domain.Models
{    
    public class Suit
    {
        //for ef
        private Suit() { }

        public Suit(string name, string description,uint order)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is not specified", nameof(name));

            Name = name;
            Description = description;
            Order = order;
            //Project = project ?? throw new ArgumentException("Suit must belogs to project", "project");
            //Childs = new List<Suit>();
            TestCases = new List<TestCase>();
        }

        public Suit(string name, string description, uint order, Suit parent)
            :this(name,description,order)
        {
            ParentSuit = parent ?? throw new ArgumentException("Parent suit is not specified", nameof(parent));
        }

        /// <summary>
        /// Suit id
        /// </summary>        
        public long Id { get; internal set; }

        /// <summary>
        /// Suit name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Suit description
        /// </summary>
        public string Description { get; private set; }

        ///// <summary>
        ///// Parent project
        ///// </summary>
        //public ProjectAggregate Project { get; private set; }

        /// <summary>
        /// Suit Order
        /// </summary>
        public uint Order { get; private set; }

        /// <summary>
        /// Parent suit
        /// </summary>
        public Suit ParentSuit { get; private set; }

        ///// <summary>
        ///// Childs
        ///// </summary>
        //public List<Suit> Childs { get; private set; }

        /// <summary>
        /// Test cases in the suit
        /// </summary>
        public List<TestCase> TestCases { get; private set; }
    }
}