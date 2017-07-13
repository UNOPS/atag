namespace ATag.EntityFrameworkCore.DataAccess
{
    using ATag.Core;
    using ATag.EntityFrameworkCore.Mappings;
    using Microsoft.EntityFrameworkCore;

    internal class TagsDbContext : DbContext
    {
        private const string DefaultConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=tag;Trusted_Connection=True;MultipleActiveResultSets=true";

        private readonly string schema;

        public TagsDbContext()
            : this(new DbContextOptionsBuilder().UseSqlServer(DefaultConnectionString).Options, "dbo")
        {

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

            builder.HasDefaultSchema(this.schema);

            builder.AddConfiguration(new TagMap());
            builder.AddConfiguration(new TaggedEntityMap());
            builder.AddConfiguration(new TagCommentMap());
        }
    }
}