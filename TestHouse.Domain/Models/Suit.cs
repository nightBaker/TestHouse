using System;

namespace TestHouse.Domain.Models
{
    public class Suit
    {
        public Suit(string name, string description, Project project)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is not specified", "name");

            Name = name;
            Description = description;
            Project = project ?? throw new ArgumentException("Suit must belogs to project", "project"); ;
        }

        /// <summary>
        /// Suit id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Suit name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Suit description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Parent project
        /// </summary>
        public Project Project { get; private set; }
    }
}