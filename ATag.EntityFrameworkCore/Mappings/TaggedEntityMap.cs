namespace ATag.EntityFrameworkCore.Mappings
{
    using ATag.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TaggedEntityMap : DbEntityConfiguration<TaggedEntity>
    {
        public const int EntityTypeMaxLength = 30;
        public const int EntityKeyMaxLength = 20;

        public override void Configure(EntityTypeBuilder<TaggedEntity> entity)
        {
            entity.ToTable("TaggedEntity");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id");
            entity.Property(t => t.CreatedOn).HasColumnName("DateCreated");
            entity.Property(t => t.EntityType).HasColumnName("EntityType").HasMaxLength(EntityTypeMaxLength).IsUnicode(false);
            entity.Property(t => t.EntityKey).HasColumnName("EntityKey").HasMaxLength(EntityKeyMaxLength).IsUnicode(false);
            entity.Property(t => t.TagId).HasColumnName("TagId");
            entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            entity.HasOne(t => t.TagComment).WithOne(t => t.TaggedEntity).HasForeignKey<TagComment>(t => t.Id);
            entity.HasIndex(t => new { t.EntityType, t.EntityKey, t.TagId }).HasName("IX_TaggedEntity_EntityType_EntityKey_TagId");
        }
    }
}