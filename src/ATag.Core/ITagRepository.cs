namespace ATag.Core;

using System.Collections.Generic;

public interface ITagRepository
{
    /// <summary>
    /// Create new tag.
    /// </summary>
    /// <param name="tag">Tag object</param>
    /// <returns>Tag Id</returns>
    int AddTag(Tag tag);

    /// <summary>
    /// Create new tag asynchronously.
    /// </summary>
    /// <param name="tag">Tag object</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>Tag Id</returns>
    Task<int> AddTagAsync(Tag tag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add tagged entity for Tag.
    /// </summary>
    /// <param name="tagIds">Tag Id(s)</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="note">Note</param>
    /// <param name="userId">User id who tag entity</param>
    void AddTaggedEntity(ICollection<int> tagIds, string entityType, string entityKey, string note, int userId);

    /// <summary>
    /// Add tagged entity for Tag.
    /// </summary>
    /// <param name="tagIds">Tag Id(s)</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="note">Note</param>
    /// <param name="userId">User id who tag entity</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddTaggedEntityAsync(ICollection<int> tagIds,
        string entityType,
        string entityKey,
        string note,
        int userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete tag.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <param name="userId">User id who deleted tag</param>
    void DeleteTag(int tagId, int userId);

    /// <summary>
    /// Delete tag.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <param name="userId">User id who deleted tag</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteTagAsync(int tagId, int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete tagged entity.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    void DeleteTaggedEntity(int tagId, string entityType, string entityKey);

    /// <summary>
    /// Delete tagged entity.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteTaggedEntityAsync(int tagId, string entityType, string entityKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Edit tag.
    /// </summary>
    /// <param name="name">Tag name</param>
    /// <param name="ownerId">Owner id</param>
    /// <param name="ownerType">Owner type</param>
    /// <param name="tagId">Tag id</param>
    /// <param name="userId">User Id who edit tag</param>
    void EditTag(string name, string ownerId, int ownerType, int tagId, int userId);

    /// <summary>
    /// Edit tag.
    /// </summary>
    /// <param name="name">Tag name</param>
    /// <param name="ownerId">Owner id</param>
    /// <param name="ownerType">Owner type</param>
    /// <param name="tagId">Tag id</param>
    /// <param name="userId">User Id who edit tag</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task EditTagAsync(string name, string ownerId, int ownerType, int tagId, int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Edit tag's note.
    /// </summary>
    /// <param name="taggedEntityId">TaggedEntity Id</param>
    /// <param name="note">Comment</param>
    /// <param name="userId">User Id</param>
    void EditTagNote(int taggedEntityId, string note, int userId);

    /// <summary>
    /// Edit tag's note.
    /// </summary>
    /// <param name="taggedEntityId">TaggedEntity Id</param>
    /// <param name="note">Comment</param>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task EditTagNoteAsync(int taggedEntityId, string note, int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Edit tag's note.
    /// </summary>
    /// <param name="tagId">Tag Id</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="note">Comment</param>
    /// <param name="userId">User Id</param>
    void EditTagNote(int tagId, string entityType, string entityKey, string note, int userId);

    /// <summary>
    /// Edit tag's note.
    /// </summary>
    /// <param name="tagId">Tag Id</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="note">Comment</param>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task EditTagNoteAsync(int tagId, string entityType, string entityKey, string note, int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get tags for the given entity.
    /// </summary>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="filters">Owners' filters</param>
    /// <returns>List of tags</returns>
    IEnumerable<Tag> LoadEntityTags(string entityType, string entityKey, params TagOwnerFilter[] filters);

    /// <summary>
    /// Get tags for the given entity.
    /// </summary>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="filters">Owners' filters</param>
    /// <returns>List of tags</returns>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<Tag[]> LoadEntityTagsAsync(string entityType, string entityKey, params TagOwnerFilter[] filters);

    /// <summary>
    /// Get tags for the given entity.
    /// </summary>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKeys">Entity keys</param>
    /// <param name="filters">Owners' filters</param>
    /// <returns>List of tags</returns>
    IEnumerable<Tag> LoadEntityTags(string entityType, string[] entityKeys, params TagOwnerFilter[] filters);

    /// <summary>
    /// Get tags for the given entity.
    /// </summary>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKeys">Entity keys</param>
    /// <param name="filters">Owners' filters</param>
    /// <returns>List of tags</returns>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<Tag[]> LoadEntityTagsAsync(string entityType, string[] entityKeys, params TagOwnerFilter[] filters);

    /// <summary>
    /// Get tags for the given entity.
    /// </summary>
    /// <param name="entityType">Entity type</param>
    /// <param name="filters">Owners' filters</param>
    /// <returns>List of tags</returns>
    IEnumerable<Tag> LoadEntityTags(string entityType, params TagOwnerFilter[] filters);

    /// <summary>
    /// Get tags for the given entity.
    /// </summary>
    /// <param name="entityType">Entity type</param>
    /// <param name="filters">Owners' filters</param>
    /// <returns>List of tags</returns>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<Tag[]> LoadEntityTagsAsync(string entityType, params TagOwnerFilter[] filters);

    /// <summary>
    /// Load tag details.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <returns>Returns tag details.</returns>
    Tag LoadTag(int tagId);

    /// <summary>
    /// Load tag details.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>Returns tag details.</returns>
    Task<Tag> LoadTagAsync(int tagId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Load tag entities.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <param name="pageIndex">current page number</param>
    /// <param name="pageSize">number of rows in page</param>
    /// <returns>Returns paginated list of tags for given filter(s).</returns>
    PagedEntity<TaggedEntity> LoadTaggedEntities(int tagId, int pageIndex, int pageSize);

    /// <summary>
    /// Load tag entities.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <param name="pageIndex">current page number</param>
    /// <param name="pageSize">number of rows in page</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>Returns paginated list of tags for given filter(s).</returns>
    Task<PagedEntity<TaggedEntity>> LoadTaggedEntitiesAsync(int tagId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Load tag entities.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <returns>Returns paginated list of tags for given filter(s).</returns>
    IEnumerable<TaggedEntity> LoadTaggedEntities(int tagId);

    /// <summary>
    /// Load tag entities.
    /// </summary>
    /// <param name="tagId">Tag id</param>
    /// <returns>Returns paginated list of tags for given filter(s).</returns>
    Task<TaggedEntity[]> LoadTaggedEntitiesAsync(int tagId);

    /// <summary>
    /// Load tag details.
    /// </summary>
    /// <param name="taggedEntityId">TaggedEntity id</param>
    /// <returns>Returns tag's note.</returns>
    string LoadTagNote(int taggedEntityId);

    /// <summary>
    /// Load tag details.
    /// </summary>
    /// <param name="taggedEntityId">TaggedEntity id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>Returns tag's note.</returns>
    Task<string> LoadTagNoteAsync(int taggedEntityId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Load tag details.
    /// </summary>
    /// <param name="tagId">Tag Id</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <returns>Returns tag's note.</returns>
    string LoadTagNote(int tagId, string entityType, string entityKey);

    /// <summary>
    /// Load tag details.
    /// </summary>
    /// <param name="tagId">Tag Id</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>Returns tag's note.</returns>
    Task<string> LoadTagNoteAsync(int tagId, string entityType, string entityKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all tags for filters.
    /// </summary>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="filters">Owners' filters</param>
    /// <returns>Returns paginated list of tags.</returns>
    PagedEntity<Tag> LoadTags(int pageIndex, int pageSize, params TagOwnerFilter[] filters);

    /// <summary>
    /// Get all tags for filters.
    /// </summary>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <param name="filters">Owners' filters</param>
    /// <returns>Returns paginated list of tags.</returns>
    Task<PagedEntity<Tag>> LoadTagsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default, params TagOwnerFilter[] filters);

    /// <summary>
    /// Get all tags for filters.
    /// </summary>
    /// <param name="filters">Owners' filters</param>
    /// <returns>Returns paginated list of tags.</returns>
    IEnumerable<Tag> LoadTags(params TagOwnerFilter[] filters);

    /// <summary>
    /// Get all tags for filters.
    /// </summary>
    /// <param name="filters">Owners' filters</param>
    /// <returns>Returns paginated list of tags.</returns>
    Task<Tag[]> LoadTagsAsync(params TagOwnerFilter[] filters);

    /// <summary>
    /// Tag an entity.
    /// </summary>
    /// <param name="tagIds">Tag name</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="note">Note</param>
    /// <param name="userId">User Id who edit tag</param>
    void TagEntity(ICollection<int> tagIds, string entityType, string entityKey, string note, int userId);

    /// <summary>
    /// Tag an entity.
    /// </summary>
    /// <param name="tagIds">Tag name</param>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityKey">Entity key</param>
    /// <param name="note">Note</param>
    /// <param name="userId">User Id who edit tag</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task TagEntityAsync(ICollection<int> tagIds,
        string entityType,
        string entityKey,
        string note,
        int userId,
        CancellationToken cancellationToken = default);
}