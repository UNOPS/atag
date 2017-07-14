namespace ATag.EntityFrameworkCore.Mappings
{
    using ATag.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TagNoteMap : DbEntityConfiguration<TagNote>
    {
        public override void Configure(EntityTypeBuilder<TagNote> entity)
        {
            entity.ToTable("TagNote");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id");
            entity.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            entity.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            entity.Property(t => t.Note).HasColumnName("Note").HasMaxLength(TagNote.NoteMaxLength);
            entity.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            entity.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
        }
    }
}