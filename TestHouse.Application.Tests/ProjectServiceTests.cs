using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestHouse.Application.Services;
using TestHouse.Persistence;
using Xunit;

namespace TestHouse.Application.Tests
{
    public class ProjectServiceTests
    {
        [Fact]
        public async Task Test1()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<TestHouseDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new TestHouseDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new TestHouseDbContext(options))
                {
                    var projectService = new ProjectService(context);
                    await projectService.AddProject("test name", "test description");
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new TestHouseDbContext(options))
                {
                    Assert.Equal(1, context.Projects.Count());
                    Assert.Equal("test name", context.Projects.Single().Name);
                    Assert.Equal("test description", context.Projects.Single().Description);
                    Assert.True(context.Projects.Single().Id > 0);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
