namespace ATag.EntityFrameworkCore.Mappings
{
    using ATag.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TagCommentMap : DbEntityConfiguration<TagComment>
    {
        private const int CommentMaxLength = 1000;

        public override void Configure(EntityTypeBuilder<TagComment> entity)
        {
            entity.ToTable("TagComment");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.CreatedOn).HasColumnName("DateCreated");
            entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            entity.Property(t => t.Comment).HasColumnName("Comment").HasMaxLength(CommentMaxLength);
            entity.Property(t => t.ModifiedOn).HasColumnName("DateModified");
            entity.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
            entity.HasOne(t => t.TaggedEntity).WithOne(t => t.TagComment).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
        }
    }
}