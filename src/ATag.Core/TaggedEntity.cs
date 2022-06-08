namespace ATag.Core
{
    using System;

    public class TaggedEntity
    {
        public const int EntityKeyMaxLength = 20;
        public const int EntityTypeMaxLength = 30;

        protected TaggedEntity()
        {
        }

        public TaggedEntity(int tagId, string key, string type, int userId)
        {
            key.EnforceMaxLength(EntityKeyMaxLength);
            type.EnforceMaxLength(EntityTypeMaxLength);

            this.CreatedByUserId = userId;
            this.CreatedOn = DateTime.UtcNow;
            this.EntityKey = key;
            this.EntityType = type;
            this.TagId = tagId;
        }

        public int CreatedByUserId { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public string EntityKey { get; protected set; }
        public string EntityType { get; protected set; }

        public int Id { get; protected set; }
        public virtual Tag Tag { get; internal set; }

        public virtual TagNote TagNote { get; internal set; }

        public int TagId { get; protected set; }

        public void SetNote(string note, int userId)
        {
            note.EnforceMaxLength(TagNote.NoteMaxLength);

            if (string.IsNullOrWhiteSpace(note))
            {
                throw new TagException("Tag's comment cannot be an empty string.");
            }

            if (this.TagNote == null)
            {
                this.TagNote = new TagNote(note, userId);
            }
            else
            {
                this.TagNote.Edit(note, userId);
            }
        }
    }
}