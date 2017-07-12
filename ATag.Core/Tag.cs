namespace ATag.Core
{
    using System;
    using System.Collections.Generic;

    public class Tag
    {
        public const int NameMaxLength = 30;
        public const int OwnerIdMaxLength = 30;

        public Tag(string name, int ownerType, string ownerId, int createdByUser)
        {
            name.EnforceMaxLength(NameMaxLength);
            ownerId.EnforceMaxLength(OwnerIdMaxLength);

            this.Name = name;
            this.DateCreated = DateTime.UtcNow;
            this.OwnerType = ownerType;
            this.OwnerId = ownerId;
            this.CreatedByUserId = createdByUser;
        }

        public int CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int? ModifiedByUserId { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public int OwnerType { get; set; }
        public virtual ICollection<TaggedEntity> TaggedEntities { get; set; }

        public void Delete(int modifiedByUser)
        {
            this.IsDeleted = true;
            this.DateModified = DateTime.UtcNow;
            this.ModifiedByUserId = modifiedByUser;
        }

        public void Edit(string name, int ownerType, string ownerId, int modifiedByUser)
        {
            name.EnforceMaxLength(NameMaxLength);
            ownerId.EnforceMaxLength(OwnerIdMaxLength);

            if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 1)
            {
                throw new TagException("Tag's name cannot be an empty string.");
            }

            this.Name = name;
            this.OwnerType = ownerType;
            this.OwnerId = ownerId;
            this.ModifiedByUserId = modifiedByUser;
            this.DateModified = DateTime.UtcNow;
        }

        public void TagEntity(string key, string type, string comment, int userId)
        {
            if (this.TaggedEntities == null)
            {
                this.TaggedEntities = new List<TaggedEntity>();
            }

            this.TaggedEntities.Add(new TaggedEntity(this.Id, key, type, userId)
            {
                TagData = this,
                TagComment = new TagComment(comment, userId)
            });
        }
    }
}