using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.Domain.Models
{
    public class Project
    {        
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


        public Project(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name is not specified", "name");

            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
