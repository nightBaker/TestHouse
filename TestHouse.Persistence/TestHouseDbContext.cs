using Microsoft.EntityFrameworkCore;
using System;
using TestHouse.Domain.Models;

namespace TestHouse.Persistence
{
    public class TestHouseDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Suit> Suits { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
    }
}
