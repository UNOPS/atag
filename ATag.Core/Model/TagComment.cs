namespace ATag.Core.Model
{
    using System;
    using ATag.Core.Helper;

    public class TagComment
    {
        public const int CommentMaxLength = 1000;

        public TagComment(string comment, int userId)
        {
            comment.EnforceMaxLength(CommentMaxLength);

            this.CreatedByUserId = userId;
            this.DateCreated = DateTime.UtcNow;
            this.Comment = comment;
        }

        public string Comment { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int Id { get; set; }
        public int? ModifiedByUserId { get; set; }
        public virtual TaggedEntity TaggedEntityData { get; set; }

        public void Edit(string comment, int userId)
        {
            comment.EnforceMaxLength(CommentMaxLength);

            this.ModifiedByUserId = userId;
            this.DateModified = DateTime.UtcNow;
            this.Comment = comment;
        }
    }
}