namespace ATag.Core.Model
{
    using System;
    using ATag.Core.Helper;

    public class TaggedEntity
    {
        public const int EntityKeyMaxLength = 20;
        public const int EntityTypeMaxLength = 30;

        public const int CommentMaxLength = 1000;

        public TaggedEntity(int tagId, string key, string type, int userId)
        {
            key.EnforceMaxLength(EntityKeyMaxLength);
            type.EnforceMaxLength(EntityTypeMaxLength);

            this.CreatedByUserId = userId;
            this.DateCreated = DateTime.UtcNow;
            this.EntityKey = key;
            this.EntityType = type;
            this.TagId = tagId;
        }

        public int CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string EntityKey { get; set; }
        public string EntityType { get; set; }

        public int Id { get; set; }

        public virtual TagComment TagComment { get; internal set; }
        public virtual Tag TagData { get; internal set; }

        public int TagId { get; set; }

        public void Comment(string comment, int userId)
        {
            comment.EnforceMaxLength(CommentMaxLength);

            if (string.IsNullOrWhiteSpace(comment) || comment.Trim().Length < 1)
            {
                throw new Exception("Tag's comment cannot be an empty string.");
            }

            if (this.TagComment == null)
            {
                this.TagComment = new TagComment(comment, userId);
            }
            else
            {
                this.TagComment.Edit(comment, userId);
            }
        }
    }
}