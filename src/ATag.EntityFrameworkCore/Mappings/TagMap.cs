namespace ATag.EntityFrameworkCore.Mappings
{
    using ATag.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TagMap : DbEntityConfiguration<Tag>
    {
	    private readonly string schema;

	    public TagMap(string schema)
	    {
		    this.schema = schema;
	    }

	    public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.ToTable("Tag", this.schema);
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.Name).HasColumnName("Name").HasMaxLength(Tag.NameMaxLength);
            entity.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            entity.Property(t => t.OwnerType).HasColumnName("OwnerType");
            entity.Property(t => t.OwnerId).HasColumnName("OwnerId").HasMaxLength(Tag.OwnerIdMaxLength).IsUnicode(false);
            entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            entity.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            entity.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            entity.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
            entity.HasMany(t => t.TaggedEntities).WithOne(t => t.Tag).HasForeignKey(t => t.TagId);
            entity.HasIndex(t => new { t.OwnerType, t.OwnerId }).HasName("IX_Tag_OwnerType_OwnerId");
        }
    }
}