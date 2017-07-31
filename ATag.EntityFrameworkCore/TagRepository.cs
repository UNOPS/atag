namespace ATag.EntityFrameworkCore
{
    using System.Collections.Generic;
    using System.Linq;
    using ATag.Core;
    using ATag.EntityFrameworkCore.DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class TagRepository : ITagRepository
    {
        internal readonly TagsDbContext DbContext;

        /// <summary>
        /// Initialize new instance of repository
        /// </summary>
        /// <param name="context"></param>
        public TagRepository(DataContext context)
        {
            this.DbContext = context.DbContext;
        }

        public int AddTag(Tag tag)
        {
            this.DbContext.Tags.Add(tag);
            this.DbContext.SaveChanges();
            return tag.Id;
        }

        public void AddTaggedEntity(ICollection<int> tagIds, string entityType, string entityKey, string note, int userId)
        {
            var existTagIds = this.DbContext.TaggedEntities
                .Where(t => t.EntityType == entityType && t.EntityKey == entityKey && tagIds.Contains(t.TagId))
                .Select(t => t.TagId)
                .ToList();

            var missingTagIds = tagIds.Except(existTagIds).ToList();

            var tags = this.DbContext.Tags
                .Where(a => missingTagIds.Contains(a.Id))
                .ToArray();

            foreach (var tag in tags)
            {
                tag.TagEntity(entityKey, entityType, note, userId);
            }

            this.DbContext.SaveChanges();
        }

        public void DeleteTag(int tagId, int userId)
        {
            var entity = this.DbContext.Tags.Find(tagId);

            if (entity == null)
            {
                return;
            }

            entity.Delete(userId);
            this.DbContext.SaveChanges();
        }

        public void DeleteTaggedEntity(int tagId, string entityType, string entityKey)
        {
            var taggedEntities = this.DbContext.TaggedEntities
                .Where(a => a.EntityType == entityType && a.EntityKey == entityKey && a.TagId == tagId)
                .ToList();

            if (taggedEntities.Count == 0)
            {
                return;
            }

            this.DbContext.TaggedEntities.RemoveRange(taggedEntities);
            this.DbContext.SaveChanges();
        }

        public void EditTag(string name, string ownerId, int ownerType, int tagId, int userId)
        {
            var tag = this.DbContext.Tags.Find(tagId);

            if (tag == null)
            {
                return;
            }

            var tagWithSameNameExists = this.DbContext.Tags.Any(t =>
                t.OwnerType == tag.OwnerType &&
                t.OwnerId == tag.OwnerId &&
                t.Name == name &&
                t.Id != tag.Id &&
                t.IsDeleted == false);

            if (tagWithSameNameExists)
            {
                throw new TagException("Tag with the same name already exists.");
            }

            tag.Edit(name, ownerType, ownerId, userId);

            this.DbContext.SaveChanges();
        }

        public void EditTagNote(int taggedEntityId, string note, int userId)
        {
            var taggedEntityData = this.DbContext.TaggedEntities
                .Include(a => a.TagNote)
                .Single(a => a.Id == taggedEntityId);

            if (taggedEntityData == null)
            {
                return;
            }

            taggedEntityData.SetNote(note, userId);

            this.DbContext.SaveChanges();
        }

        public void EditTagNote(string entityType, string entityKey, string note, int userId)
        {
            var taggedEntityData = this.DbContext.TaggedEntities
                .Include(a => a.TagNote)
                .Single(a => a.EntityType.Equals(entityType) && a.EntityKey.Equals(entityKey));

            if (taggedEntityData == null)
            {
                return;
            }

            taggedEntityData.SetNote(note, userId);

            this.DbContext.SaveChanges();
        }

        public IEnumerable<Tag> LoadEntityTags(string entityType, string entityKey, params TagOwnerFilter[] filters)
        {
            return this.DbContext.Tags
                .BelongingTo(filters.ToArray())
                .Where(a => !a.IsDeleted && a.TaggedEntities.Any(t => t.EntityKey == entityKey && t.EntityType == entityType))
                .ToArray();
        }

        public Tag LoadTag(int tagId)
        {
            return this.DbContext.Tags.FirstOrDefault(a => a.Id == tagId && !a.IsDeleted);
        }

        public IEnumerable<TaggedEntity> LoadTaggedEntities(int tagId)
        {
            return this.DbContext.TaggedEntities
                .Include(a => a.TagNote)
                .Where(t => t.TagId == tagId)
                .OrderByDescending(t => t.Id)
                .ToArray();
        }

        public string LoadTagNote(int taggedEntityId)
        {
            return this.DbContext.TaggedEntities.Include(a => a.TagNote).SingleOrDefault(a => a.Id == taggedEntityId)?.TagNote?.Note;
        }

        public string LoadTagNote(string entityType, string entityKey)
        {
            return this.DbContext.TaggedEntities
                .Include(a => a.TagNote)
                .SingleOrDefault(a => a.EntityType.Equals(entityType) && a.EntityKey.Equals(entityKey))?.TagNote?.Note;
        }

        public PagedEntity<TaggedEntity> LoadTaggedEntities(int tagId, int pageIndex, int pageSize)
        {
            return this.DbContext.TaggedEntities
                .Include(a => a.TagNote)
                .Where(t => t.TagId == tagId)
                .OrderByDescending(t => t.Id)
                .WithPaging(pageIndex, pageSize);
        }

        public PagedEntity<Tag> LoadTags(int pageIndex, int pageSize, params TagOwnerFilter[] filters)
        {
            return this.DbContext.Tags
                .BelongingTo(filters.ToArray())
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Id)
                .WithPaging(pageIndex, pageSize);
        }

        public IEnumerable<Tag> LoadTags(params TagOwnerFilter[] filters)
        {
            return this.DbContext.Tags
                .BelongingTo(filters.ToArray())
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Id)
                .ToArray();
        }

        public void TagEntity(ICollection<int> tagIds, string entityType, string entityKey, string note, int userId)
        {
            var existingTagIds = this.DbContext.TaggedEntities
                .Where(t => t.EntityType == entityType && t.EntityKey == entityKey && tagIds.Contains(t.TagId))
                .Select(t => t.TagId)
                .ToList();

            var missingTagIds = tagIds.Except(existingTagIds).ToList();

            var tags = this.DbContext.Tags
                .Where(a => missingTagIds.Contains(a.Id))
                .ToArray();

            foreach (var tag in tags)
            {
                tag.TagEntity(entityKey, entityType, note, userId);
            }

            this.DbContext.SaveChanges();
        }
    }
}