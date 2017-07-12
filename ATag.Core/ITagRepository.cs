namespace ATag.Core
{
    using System.Collections.Generic;

    public interface ITagRepository
    {
        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="name">Tag name</param>
        /// <param name="ownerType">Owner type (e.g. Private or Public)</param>
        /// <param name="ownerId">Owner id (e.g. UserId or RoleName)</param>
        /// <param name="userId">User Id who creates tag</param>
        /// <returns>Created tag.</returns>
        Tag AddTag(string name, int ownerType, string ownerId, int userId);

        /// <summary>
        /// Add tagged entity for Tag
        /// </summary>
        /// <param name="tagIds">Tag Id(s)</param>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        /// <param name="comment">Comment</param>
        /// <param name="userId">User id</param>
        void AddTaggedEntity(ICollection<int> tagIds, string entityType, string entityKey, string comment, int userId);

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <param name="userId">User Id</param>
        void DeleteTag(int tagId, int userId);

        /// <summary>
        /// Delete tagged entity
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        void DeleteTaggedEntity(int tagId, string entityType, string entityKey);

        /// <summary>
        /// Edit tag
        /// </summary>
        /// <param name="name">Tag name</param>
        /// <param name="ownerId">Owner id</param>
        /// <param name="ownerType">Owner type</param>
        /// <param name="tagId">Tag id</param>
        /// <param name="userId">User Id who edit tag</param>
        void EditTag(string name, string ownerId, int ownerType, int tagId, int userId);

        /// <summary>
        /// Edit tag's comment
        /// </summary>
        /// <param name="taggedEntityId">TaggedEntity Id</param>
        /// <param name="comment">Comment</param>
        /// <param name="userId">User Id</param>
        /// <param name="entityKey">Entity key</param>
        void EditTagComment(int taggedEntityId, string comment, int userId, string entityKey = null);

        /// <summary>
        /// Get list of tags of Entity
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">current page number</param>
        /// <param name="filters">number of rows in page</param>
        /// <returns>Returns paginated list of tags for given filter(s).</returns>
        IEnumerable<Tag> LoadEntityTags(string entityType, string entityKey, params TagOwnerFilter[] filters);

        /// <summary>
        /// Load tag details
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <returns>Returns tag details.</returns>
        Tag LoadTag(int tagId);

        /// <summary>
        /// Load tag details
        /// </summary>
        /// <param name="taggedEntityId">TaggedEntity id</param>
        /// <param name="entityKey">Entity key</param>
        /// <returns>Returns tag's comment.</returns>
        string LoadTagComment(int taggedEntityId, string entityKey);

        /// <summary>
        /// Load tag entities
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <param name="pageIndex">current page number</param>
        /// <param name="pageSize">number of rows in page</param>
        /// <returns>Returns paginated list of tags for given filter(s).</returns>
        PagedEntity<TaggedEntity> LoadTaggedEntities(int tagId, int pageIndex, int pageSize);

        /// <summary>
        /// Get all tags for filters
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns>Returns paginated list of tags for given filter(s).</returns>
        PagedEntity<Tag> LoadTags(int pageIndex, int pageSize, params TagOwnerFilter[] filters);

        /// <summary>
        /// Tag entity
        /// </summary>
        /// <param name="tagIds">Tag name</param>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityKey">Entity key</param>
        /// <param name="comment">Comment</param>
        /// <param name="userId">User Id who edit tag</param>
        void TagEntity(ICollection<int> tagIds, string entityType, string entityKey, string comment, int userId);
    }
}