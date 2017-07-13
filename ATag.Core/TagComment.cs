namespace ATag.Core
{
    using System;

    public class TagComment
    {
        public const int CommentMaxLength = 1000;

        protected TagComment()
        {
        }

        public TagComment(string comment, int userId)
        {
            comment.EnforceMaxLength(CommentMaxLength);

            this.CreatedByUserId = userId;
            this.CreatedOn = DateTime.UtcNow;
            this.Comment = comment;
        }

        public string Comment { get; protected set; }
        public int CreatedByUserId { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public int Id { get; protected set; }
        public int? ModifiedByUserId { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public virtual TaggedEntity TaggedEntity { get; protected set; }
        public int TaggedEntityDataId { get; protected set; }

        public void Edit(string comment, int userId)
        {
            comment.EnforceMaxLength(CommentMaxLength);

            this.ModifiedByUserId = userId;
            this.ModifiedOn = DateTime.UtcNow;
            this.Comment = comment;
        }
    }
}