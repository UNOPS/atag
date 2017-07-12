namespace ATag.EntityFrameworkCore
{
    using System.Threading.Tasks;
    using ATag.EntityFrameworkCore.DataAccess;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents a single unit of work.
    /// </summary>
    public class DataContext
    {
        /// <summary>
        /// Instantiates a new instance of the DataContext class.
        /// </summary>
        public DataContext(DbContextOptions options)
        {
            this.DbContext = new TagsDbContext(options);
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        internal TagsDbContext DbContext { get; private set; }

        /// <summary>
        /// Runs <see cref="RelationalDatabaseFacadeExtensions.Migrate"/> on the underlying <see cref="Microsoft.EntityFrameworkCore.DbContext"/>,
        /// thus making sure database exists and all migrations are run.
        /// </summary>
        public void MigrateDatabase()
        {
            this.DbContext.Database.Migrate();
        }

        /// <summary>
        /// Runs <see cref="RelationalDatabaseFacadeExtensions.MigrateAsync"/> on the underlying <see cref="Microsoft.EntityFrameworkCore.DbContext"/>,
        /// thus making sure database exists and all migrations are run.
        /// </summary>
        public Task MigrateDatabaseAsync()
        {
            return this.DbContext.Database.MigrateAsync();
        }
    }
}