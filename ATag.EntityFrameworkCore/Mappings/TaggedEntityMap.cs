namespace ATag.EntityFrameworkCore.Mappings
{
    using ATag.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TaggedEntityMap : DbEntityConfiguration<TaggedEntity>
    {
        public const int EntityTypeMaxLength = 30;
        public const int EntityKeyMaxLength = 20;
        private const string IndexName = "IX_TaggedEntity_EntityType_EntityKey_TagId";

        public override void Configure(EntityTypeBuilder<TaggedEntity> entity, string schema)
        {
            entity.ToTable("TaggedEntity", schema);
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.DateCreated).HasColumnName("DateCreated");
            entity.Property(t => t.EntityType).HasColumnName("EntityType").HasMaxLength(EntityTypeMaxLength).IsUnicode(false);
            entity.Property(t => t.EntityKey).HasColumnName("EntityKey").HasMaxLength(EntityKeyMaxLength).IsUnicode(false);
            entity.Property(t => t.TagId).HasColumnName("TagId");
            entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            entity.HasOne(t => t.TagComment).WithOne(t => t.TaggedEntityData).HasForeignKey<TagComment>(t => t.TaggedEntityDataId);
        }
    }
}