namespace ATag.EntityFrameworkCore.DataAccess
{
    using ATag.Core.Model;
    using ATag.EntityFrameworkCore.Mappings;
    using Microsoft.EntityFrameworkCore;

    public class TagsDbContext : DbContext
    {
        public TagsDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("tag");

            builder.AddConfiguration(new TagMap());
        }
    }
}