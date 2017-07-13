namespace ATag.EntityFrameworkCore.Mappings
{
    using ATag.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TagCommentMap : DbEntityConfiguration<TagComment>
    {
        private const int CommentMaxLength = 1000;

        public override void Configure(EntityTypeBuilder<TagComment> entity)
        {
            entity.ToTable("TagComment");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").ValueGeneratedNever();
            entity.Property(t => t.CreatedOn).HasColumnName("DateCreated");
            entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            entity.Property(t => t.Comment).HasColumnName("Comment").HasMaxLength(CommentMaxLength);
            entity.Property(t => t.ModifiedOn).HasColumnName("DateModified");
            entity.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
        }
    }
}