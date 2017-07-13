namespace ATag.Core
{
    using System;
    using System.Collections.Generic;

    public class Tag
    {
        public const int NameMaxLength = 30;
        public const int OwnerIdMaxLength = 30;

        protected Tag()
        {
        }

        public Tag(string name, int ownerType, string ownerId, int createdByUser)
        {
            name.EnforceMaxLength(NameMaxLength);
            ownerId.EnforceMaxLength(OwnerIdMaxLength);

            this.Name = name;
            this.CreatedOn = DateTime.UtcNow;
            this.OwnerType = ownerType;
            this.OwnerId = ownerId;
            this.CreatedByUserId = createdByUser;
        }

        public int CreatedByUserId { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public int Id { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public int? ModifiedByUserId { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public string Name { get; protected set; }
        public string OwnerId { get; protected set; }
        public int OwnerType { get; protected set; }
        public virtual ICollection<TaggedEntity> TaggedEntities { get; protected set; }

        public void Delete(int modifiedByUser)
        {
            this.IsDeleted = true;
            this.ModifiedOn = DateTime.UtcNow;
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
            this.ModifiedOn = DateTime.UtcNow;
        }

        public void TagEntity(string key, string type, string comment, int userId)
        {
            if (this.TaggedEntities == null)
            {
                this.TaggedEntities = new List<TaggedEntity>();
            }

            this.TaggedEntities.Add(new TaggedEntity(this.Id, key, type, userId)
            {
                Tag = this,
                TagNote = new TagNote(comment, userId)
            });
        }
    }
}