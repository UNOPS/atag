namespace ATag.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Manage tags for a repository 
    /// </summary>
    public class TagService
    {
        //Tag repository
        private readonly ITagRepository tagRepository;

        /// <summary>
        /// Initialize new instance of tag service
        /// </summary>
        /// <param name="tagRepository">Service's repository</param>
        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public Tag AddTag(string tagName, string ownerId, int ownerType, int userId)
        {
            return this.tagRepository.AddTag(tagName, ownerType, ownerId, userId);
        }

        public void AddTaggedEntity(ICollection<int> tagIds, string entityType, string entityKey, string comment, int userId)
        {
            this.tagRepository.AddTaggedEntity(tagIds, entityType, entityKey, comment, userId);
        }

        public void DeleteTag(int tagId, int userId)
        {
            this.tagRepository.DeleteTag(tagId, userId);
        }

        public void DeleteTaggedEntity(int tagId, string entityType, string entityKey)
        {
            this.tagRepository.DeleteTaggedEntity(tagId, entityType, entityKey);
        }

        public void EditTag(string tagName, string ownerId, int ownerType, int tagId, int userId)
        {
            this.tagRepository.EditTag(tagName, ownerId, ownerType, tagId, userId);
        }

        public void EditTagComment(int taggedEntityId, string comment, int userId, string entityKey = null)
        {
            this.tagRepository.EditTagComment(taggedEntityId, comment, userId, entityKey);
        }

        public PagedEntity<Tag> GetTags(int pageIndex, int pageSize, params TagOwnerFilter[] filters)
        {
            return this.tagRepository.LoadTags(pageIndex, pageSize, filters);
        }

        public IEnumerable<Tag> LoadEntityTags(string entityType, string entityKey, params TagOwnerFilter[] filters)
        {
            return this.tagRepository.LoadEntityTags(entityType, entityKey, filters);
        }

        public Tag LoadTag(int tagId)
        {
            return this.tagRepository.LoadTag(tagId);
        }

        public string LoadTagComment(int taggedEntityId, string entityKey)
        {
            return this.tagRepository.LoadTagComment(taggedEntityId, entityKey);
        }

        public PagedEntity<TaggedEntity> LoadTagEntities(int tagId, int pageIndex, int pageSize)
        {
            return this.tagRepository.LoadTaggedEntities(tagId, pageIndex, pageSize);
        }

        public void TagEntity(ICollection<int> tagIds, string entityType, string entityKey, string comment, int createdByUserId)
        {
            this.tagRepository.TagEntity(tagIds, entityType, entityKey, comment, createdByUserId);
        }
    }
}