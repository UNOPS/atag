namespace ATag.EntityFrameworkCore.Mappings
{
	using ATag.Core;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal class TaggedEntityMap : DbEntityConfiguration<TaggedEntity>
	{
		private readonly string schema;

		public TaggedEntityMap(string schema)
		{
			this.schema = schema;
		}

		public override void Configure(EntityTypeBuilder<TaggedEntity> entity)
		{
			entity.ToTable("TaggedEntity", this.schema);
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
			entity.Property(t => t.EntityType).HasColumnName("EntityType").HasMaxLength(TaggedEntity.EntityTypeMaxLength).IsUnicode(false);
			entity.Property(t => t.EntityKey).HasColumnName("EntityKey").HasMaxLength(TaggedEntity.EntityKeyMaxLength).IsUnicode(false);
			entity.Property(t => t.TagId).HasColumnName("TagId");
			entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
			entity.HasOne(t => t.TagNote).WithOne(t => t.TaggedEntity).HasForeignKey<TagNote>(t => t.Id);
			entity.HasIndex(t => new { t.EntityType, t.EntityKey, t.TagId }).HasName("IX_TaggedEntity_EntityType_EntityKey_TagId");
		}
	}
}