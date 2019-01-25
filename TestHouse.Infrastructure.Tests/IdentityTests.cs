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
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<AppIdentityDbContext>()
                .UseSqlite(connection)
                .Options;

                using (var context = new AppIdentityDbContext(options))
                {
                    context.Database.EnsureCreated();
                    var user = new ApplicationUser("feisal");
                    context.Add(user);
                    context.SaveChanges();
                    Assert.NotEmpty(context.Users);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
