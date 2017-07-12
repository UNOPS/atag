namespace ATag.EntityFrameworkCore.Mappings
{
    using ATag.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TagCommentMap : DbEntityConfiguration<TagComment>
    {
        private const int CommentMaxLength = 1000;

        public override void Configure(EntityTypeBuilder<TagComment> entity, string schema)
        {
            entity.ToTable("TagComment", schema);
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.DateCreated).HasColumnName("DateCreated");
            entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            entity.Property(t => t.Comment).HasColumnName("Comment").HasMaxLength(CommentMaxLength);
            entity.Property(t => t.DateModified).HasColumnName("DateModified");
            entity.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
        }
    }
}