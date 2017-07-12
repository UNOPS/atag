namespace ATag.Tests
{
    using System.IO;
    using ATag.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;


    public class DatabaseFixture
    {
        private readonly string schema;
        private readonly DbContextOptions options;

        public DatabaseFixture(string schema)
        {
            this.schema = schema;
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            this.options = new DbContextOptionsBuilder().UseSqlServer(config.GetConnectionString("tag")).Options;
        }

        public DataContext CreateDataContext()
        {
            return new DataContext(this.options, this.schema);
        }
    }
}