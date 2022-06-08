namespace ATag.Core
{
    using System;

    public class TagNote
    {
        public const int NoteMaxLength = 1000;

        protected TagNote()
        {
        }

        public TagNote(string note, int userId)
        {
            note.EnforceMaxLength(NoteMaxLength);

            this.CreatedByUserId = userId;
            this.CreatedOn = DateTime.UtcNow;
            this.Note = note;
        }

        public string Note { get; protected set; }
        public int CreatedByUserId { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public int Id { get; protected set; }
        public int? ModifiedByUserId { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public virtual TaggedEntity TaggedEntity { get; protected set; }

        public void Edit(string note, int userId)
        {
            note.EnforceMaxLength(NoteMaxLength);

            this.ModifiedByUserId = userId;
            this.ModifiedOn = DateTime.UtcNow;
            this.Note = note;
        }
    }
}