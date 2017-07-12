namespace ATag.EntityFrameworkCore.DataAccess
{
    using ATag.Core;
    using ATag.EntityFrameworkCore.Mappings;
    using Microsoft.EntityFrameworkCore;

    public class TagsDbContext : DbContext
    {
        private const string DefaultConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=tag;Trusted_Connection=True;MultipleActiveResultSets=true";

        private readonly string schema;

        public TagsDbContext(string schema)
            : base(new DbContextOptionsBuilder().UseSqlServer(DefaultConnectionString).Options)
        {
            this.schema = schema;
        }

        public TagsDbContext(DbContextOptions options, string schema) : base(options)
        {
            this.schema = schema;
        }

        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TaggedEntity> TaggedEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("tag");

            builder.AddConfiguration(new TagMap(), this.schema);
            builder.AddConfiguration(new TaggedEntityMap(), this.schema);
            builder.AddConfiguration(new TagCommentMap(), this.schema);
        }
    }
}