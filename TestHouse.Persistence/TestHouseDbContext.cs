using Microsoft.EntityFrameworkCore;
using System;
using TestHouse.Domain.Models;

namespace TestHouse.Persistence
{
    public class TestHouseDbContext : DbContext
    {
        public DbSet<ProjectAggregate> Projects { get; set; }
        public DbSet<Suit> Suits { get; set; }
        public DbSet<TestCase> TestCases { get; set; }

        public DbSet<TestRun> TestRuns { get; set; }

        public DbSet<TestRunCase> TestRunCases { get; set; }
    }
}
