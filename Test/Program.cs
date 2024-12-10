using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Test
{
    public class DatabaseConnectionTests
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnectionTests()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true);

            _configuration = configurationBuilder.Build();
        }

        [Fact]
        public void TestDatabaseConnection()
        {
            var options = new DbContextOptionsBuilder<SalesDbContext>()
                .UseNpgsql(_configuration.GetConnectionString("SalesDb"))
                .Options;

            using (var context = new SalesDbContext(options))
            {
                Assert.True(context.Database.CanConnect());
            }
        }
    }
}