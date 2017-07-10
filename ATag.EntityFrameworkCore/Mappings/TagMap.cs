namespace ATag.EntityFrameworkCore.Mappings
{
    using ATag.Core.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TagMap : DbEntityConfiguration<Tag>
    {
        private const string IndexName = "IX_Tag_OwnerType_OwnerId";

        public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.ToTable("Tag");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.Name).HasColumnName("Name").HasMaxLength(Tag.NameMaxLength).IsUnicode(false);
            entity.Property(t => t.DateCreated).HasColumnName("DateCreated");
            entity.Property(t => t.OwnerType).HasColumnName("OwnerType");
            entity.Property(t => t.OwnerId).HasColumnName("OwnerId").HasMaxLength(Tag.OwnerIdMaxLength).IsUnicode(false);
            entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            entity.Property(t => t.DateModified).HasColumnName("DateModified");
            entity.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            entity.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");

            entity.HasIndex(IndexName);
        }
    }
}