namespace ATag.Core
{
    using System;

    public class TaggedEntity
    {
        public const int EntityKeyMaxLength = 20;
        public const int EntityTypeMaxLength = 30;
        public const int CommentMaxLength = 1000;

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

        public virtual TagComment TagComment { get; internal set; }

        public int TagId { get; protected set; }

        public void Comment(string comment, int userId)
        {
            comment.EnforceMaxLength(CommentMaxLength);

            if (string.IsNullOrWhiteSpace(comment) || comment.Trim().Length < 1)
            {
                throw new TagException("Tag's comment cannot be an empty string.");
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