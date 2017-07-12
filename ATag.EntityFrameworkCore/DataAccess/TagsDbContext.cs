namespace ATag.EntityFrameworkCore.DataAccess
{
    using ATag.Core;
    using ATag.EntityFrameworkCore.Mappings;
    using Microsoft.EntityFrameworkCore;

    public class TagsDbContext : DbContext
    {
        private const string DefaultConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=tag;Trusted_Connection=True;MultipleActiveResultSets=true";

        private const string Schema = "tag";

        public TagsDbContext()
            : base(new DbContextOptionsBuilder().UseSqlServer(DefaultConnectionString).Options)
        {

        }

        public TagsDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TaggedEntity> TaggedEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("tag");

            builder.AddConfiguration(new TagMap(), Schema);
            builder.AddConfiguration(new TaggedEntityMap(), Schema);
            builder.AddConfiguration(new TagCommentMap(), Schema);
        }
    }
}