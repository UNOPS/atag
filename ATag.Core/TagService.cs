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

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tagName">Tag's name</param>
        /// <param name="ownerId">Owner Id</param>
        /// <param name="ownerType">Owner type <remarks>e.g. 1:User, 2:Group</remarks></param>
        /// <param name="userId">User id who creates tag</param>
        /// <returns>Tag Id</returns>
        public int AddTag(string tagName, string ownerId, int ownerType, int userId)
        {
            var tag = new Tag(tagName, ownerType, ownerId, userId);
            return this.tagRepository.AddTag(tag);
        }

        /// <summary>
        /// Add tagged entity for Tag
        /// </summary>
        /// <param name="tagIds">Tag Id(s)</param>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        /// <param name="note">Note</param>
        /// <param name="userId">User id who tagged entity</param>
        public void AddTaggedEntity(ICollection<int> tagIds, string entityType, string entityKey, string note, int userId)
        {
            this.tagRepository.AddTaggedEntity(tagIds, entityType, entityKey, note, userId);
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <param name="userId">User id who deleted tag</param>
        public void DeleteTag(int tagId, int userId)
        {
            this.tagRepository.DeleteTag(tagId, userId);
        }

        /// <summary>
        /// Delete tagged entity
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        public void DeleteTaggedEntity(int tagId, string entityType, string entityKey)
        {
            this.tagRepository.DeleteTaggedEntity(tagId, entityType, entityKey);
        }

        /// <summary>
        /// Edit tag
        /// </summary>
        /// <param name="name">Tag name</param>
        /// <param name="ownerId">Owner id</param>
        /// <param name="ownerType">Owner type</param>
        /// <param name="tagId">Tag id</param>
        /// <param name="userId">User Id who edit tag</param>
        public void EditTag(string name, string ownerId, int ownerType, int tagId, int userId)
        {
            this.tagRepository.EditTag(name, ownerId, ownerType, tagId, userId);
        }

        /// <summary>
        /// Edit tag's note
        /// </summary>
        /// <param name="taggedEntityId">TaggedEntity Id</param>
        /// <param name="note">Comment</param>
        /// <param name="userId">User Id</param>
        public void EditTagNote(int taggedEntityId, string note, int userId)
        {
            this.tagRepository.EditTagNote(taggedEntityId, note, userId);
        }

        /// <summary>
        /// Edit tag's comment
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        /// <param name="note">Comment</param>
        /// <param name="userId">User Id</param>
        public void EditTagNote(string entityType, string entityKey, string note, int userId)
        {
            this.tagRepository.EditTagNote(entityType, entityKey, note, userId);
        }

        /// <summary>
        /// Get list of tags of Entity
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        /// <param name="filters">Owners' filters</param>
        /// <returns>List of tags</returns>
        public IEnumerable<Tag> LoadEntityTags(string entityType, string entityKey, params TagOwnerFilter[] filters)
        {
            return this.tagRepository.LoadEntityTags(entityType, entityKey, filters);
        }

        /// <summary>
        /// Load tag details
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <returns>Returns tag details.</returns>
        public Tag LoadTag(int tagId)
        {
            return this.tagRepository.LoadTag(tagId);
        }

        /// <summary>
        /// Load tag entities
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <param name="pageIndex">current page number</param>
        /// <param name="pageSize">number of rows in page</param>
        /// <returns>Returns paginated list of tags for given filter(s).</returns>
        public PagedEntity<TaggedEntity> LoadTagEntities(int tagId, int pageIndex, int pageSize)
        {
            return this.tagRepository.LoadTaggedEntities(tagId, pageIndex, pageSize);
        }

        /// <summary>
        /// Load tag details
        /// </summary>
        /// <param name="taggedEntityId">TaggedEntity id</param>
        /// <returns>Returns tag's note.</returns>
        public string LoadTagNote(int taggedEntityId)
        {
            return this.tagRepository.LoadTagNote(taggedEntityId);
        }

        /// <summary>
        /// Load tag details
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        /// <returns>Returns tag's note.</returns>
        public string LoadTagNote(string entityType, string entityKey)
        {
            return this.tagRepository.LoadTagNote(entityType, entityKey);
        }

        /// <summary>
        /// Get all tags for filters
        /// </summary>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="filters">Owners' filters</param>
        /// <returns>Returns paginated list of tags.</returns>
        public PagedEntity<Tag> LoadTags(int pageIndex, int pageSize, params TagOwnerFilter[] filters)
        {
            return this.tagRepository.LoadTags(pageIndex, pageSize, filters);
        }

        /// <summary>
        /// Tag entity
        /// </summary>
        /// <param name="tagIds">Tag name</param>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        /// <param name="note">Note</param>
        /// <param name="userId">User Id who edit tag</param>
        public void TagEntity(ICollection<int> tagIds, string entityType, string entityKey, string note, int userId)
        {
            this.tagRepository.TagEntity(tagIds, entityType, entityKey, note, userId);
        }
    }
}