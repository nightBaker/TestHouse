using System;
using Microsoft.Data.Sqlite;
using Xunit;
using TestHouse.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace TestHouse.Infrastructure.Tests
{
    public class IdentityTests
    {
        [Fact]
        public void CreateUser()
        {
            
            try
            {
                var options = new DbContextOptionsBuilder<AppIdentityDbContext>().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestHouseDb;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=True;").Options;
                using (var context = new AppIdentityDbContext(options))
                {
                    context.Database.EnsureCreated();
                    var user = new ApplicationUser("feisal");
                    context.Add(user);
                    context.SaveChanges();
                    }

                
            }
            finally
            {
            }
        }
    }
}
