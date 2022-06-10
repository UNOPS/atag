namespace ATag.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using ATag.Core;
using ATag.EntityFrameworkCore.DataAccess;
using Microsoft.EntityFrameworkCore;

public class TagRepository : ITagRepository
{
    private readonly TagsDbContext dbContext;

    /// <summary>
    /// Initialize new instance of repository
    /// </summary>
    /// <param name="context"></param>
    public TagRepository(DataContext context)
    {
        this.dbContext = context.DbContext;
    }

    /// <inheritdoc />
    public int AddTag(Tag tag)
    {
        this.dbContext.Tags.Add(tag);
        this.dbContext.SaveChanges();

        return tag.Id;
    }

    /// <inheritdoc />
    public async Task<int> AddTagAsync(Tag tag, CancellationToken cancellationToken = default)
    {
        await this.dbContext.Tags.AddAsync(tag, cancellationToken);
        await this.dbContext.SaveChangesAsync(cancellationToken);

        return tag.Id;
    }

    /// <inheritdoc />
    public void AddTaggedEntity(ICollection<int> tagIds, string entityType, string entityKey, string note, int userId)
    {
        var existTagIds = this.dbContext.TaggedEntities
            .FilterTaggedEntities(tagIds, entityType, entityKey)
            .Select(t => t.TagId)
            .ToList();

        var missingTagIds = tagIds.Except(existTagIds).ToList();

        var tags = this.dbContext.Tags
            .Where(a => missingTagIds.Contains(a.Id))
            .ToArray();

        foreach (var tag in tags)
        {
            tag.TagEntity(entityKey, entityType, note, userId);
        }

        this.dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task AddTaggedEntityAsync(ICollection<int> tagIds,
        string entityType,
        string entityKey,
        string note,
        int userId,
        CancellationToken cancellationToken = default)
    {
        var existTagIds = await this.dbContext.TaggedEntities
            .FilterTaggedEntities(tagIds, entityType, entityKey)
            .Select(t => t.TagId)
            .ToListAsync(cancellationToken);

        var missingTagIds = tagIds.Except(existTagIds).ToList();

        var tags = this.dbContext.Tags
            .Where(a => missingTagIds.Contains(a.Id))
            .AsAsyncEnumerable();

        await foreach (var tag in tags.WithCancellation(cancellationToken))
        {
            tag.TagEntity(entityKey, entityType, note, userId);
        }

        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public void DeleteTag(int tagId, int userId)
    {
        var entity = this.dbContext.Tags.Find(tagId);

        if (entity is null)
        {
            return;
        }

        entity.Delete(userId);
        this.dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task DeleteTagAsync(int tagId, int userId, CancellationToken cancellationToken = default)
    {
        var entity = await this.dbContext.Tags.FindAsync(tagId);

        if (entity is null)
        {
            return;
        }

        entity.Delete(userId);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public void DeleteTaggedEntity(int tagId, string entityType, string entityKey)
    {
        var taggedEntities = this.dbContext.TaggedEntities
            .FilterTaggedEntities(tagId, entityType, entityKey)
            .ToList();

        if (taggedEntities.Count == 0)
        {
            return;
        }

        this.dbContext.TaggedEntities.RemoveRange(taggedEntities);
        this.dbContext.SaveChanges();
    }

    public async Task DeleteTaggedEntityAsync(int tagId, string entityType, string entityKey, CancellationToken cancellationToken = default)
    {
        var taggedEntities = await this.dbContext.TaggedEntities
            .FilterTaggedEntities(tagId, entityType, entityKey)
            .ToListAsync(cancellationToken);

        if (taggedEntities.Count == 0)
        {
            return;
        }

        this.dbContext.TaggedEntities.RemoveRange(taggedEntities);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public void EditTag(string name, string ownerId, int ownerType, int tagId, int userId)
    {
        var tag = this.dbContext.Tags.Find(tagId);

        if (tag is null)
        {
            return;
        }

        var tagWithSameNameExists = this.dbContext.Tags.Any(t =>
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

        this.dbContext.SaveChanges();
    }

    public async Task EditTagAsync(string name, string ownerId, int ownerType, int tagId, int userId, CancellationToken cancellationToken = default)
    {
        var tag = await this.dbContext.Tags.FindAsync(tagId);

        if (tag is null)
        {
            return;
        }

        var tagWithSameNameExists = await this.dbContext.Tags.AnyAsync(t =>
            t.OwnerType == tag.OwnerType &&
            t.OwnerId == tag.OwnerId &&
            t.Name == name &&
            t.Id != tag.Id &&
            t.IsDeleted == false, cancellationToken);

        if (tagWithSameNameExists)
        {
            throw new TagException("Tag with the same name already exists.");
        }

        tag.Edit(name, ownerType, ownerId, userId);

        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public void EditTagNote(int taggedEntityId, string note, int userId)
    {
        var taggedEntityData = this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .SingleOrDefault(a => a.Id == taggedEntityId);

        if (taggedEntityData is null)
        {
            return;
        }

        taggedEntityData.SetNote(note, userId);

        this.dbContext.SaveChanges();
    }

    public async Task EditTagNoteAsync(int taggedEntityId, string note, int userId, CancellationToken cancellationToken = default)
    {
        var taggedEntityData = await this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .SingleOrDefaultAsync(a => a.Id == taggedEntityId, cancellationToken);

        if (taggedEntityData is null)
        {
            return;
        }

        taggedEntityData.SetNote(note, userId);

        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public void EditTagNote(int tagId, string entityType, string entityKey, string note, int userId)
    {
        var taggedEntityData = this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .SingleOrDefault(a => a.TagId == tagId && a.EntityType.Equals(entityType) && a.EntityKey.Equals(entityKey));

        if (taggedEntityData is null)
        {
            return;
        }

        taggedEntityData.SetNote(note, userId);

        this.dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task EditTagNoteAsync(int tagId,
        string entityType,
        string entityKey,
        string note,
        int userId,
        CancellationToken cancellationToken = default)
    {
        var taggedEntityData = await this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .SingleOrDefaultAsync(a => a.TagId == tagId && a.EntityType.Equals(entityType) && a.EntityKey.Equals(entityKey), cancellationToken);

        if (taggedEntityData is null)
        {
            return;
        }

        taggedEntityData.SetNote(note, userId);

        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public IEnumerable<Tag> LoadEntityTags(string entityType, string entityKey, params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted && a.TaggedEntities.Any(t => t.EntityKey == entityKey && t.EntityType == entityType))
            .ToArray();
    }

    /// <inheritdoc />
    public Task<Tag[]> LoadEntityTagsAsync(string entityType, string entityKey, params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted && a.TaggedEntities.Any(t => t.EntityKey == entityKey && t.EntityType == entityType))
            .ToArrayAsync();
    }

    /// <inheritdoc />
    public IEnumerable<Tag> LoadEntityTags(string entityType, string[] entityKeys, params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .Include(a => a.TaggedEntities)
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted && a.TaggedEntities.Any(t => entityKeys.Contains(t.EntityKey) && t.EntityType == entityType))
            .ToArray();
    }

    /// <inheritdoc />
    public Task<Tag[]> LoadEntityTagsAsync(string entityType, string[] entityKeys, params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .Include(a => a.TaggedEntities)
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted && a.TaggedEntities.Any(t => entityKeys.Contains(t.EntityKey) && t.EntityType == entityType))
            .ToArrayAsync();
    }

    /// <inheritdoc />
    public IEnumerable<Tag> LoadEntityTags(string entityType, params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .Include(a => a.TaggedEntities)
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted && a.TaggedEntities.Any(t => t.EntityType == entityType))
            .ToArray();
    }

    /// <inheritdoc />
    public Task<Tag[]> LoadEntityTagsAsync(string entityType, params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .Include(a => a.TaggedEntities)
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted && a.TaggedEntities.Any(t => t.EntityType == entityType))
            .ToArrayAsync();
    }

    /// <inheritdoc />
    public Tag LoadTag(int tagId)
    {
        return this.dbContext.Tags.FirstOrDefault(a => a.Id == tagId && !a.IsDeleted);
    }

    /// <inheritdoc />
    public Task<Tag> LoadTagAsync(int tagId, CancellationToken cancellationToken = default)
    {
        return this.dbContext.Tags.FirstOrDefaultAsync(a => a.Id == tagId && !a.IsDeleted, cancellationToken);
    }

    /// <inheritdoc />
    public PagedEntity<TaggedEntity> LoadTaggedEntities(int tagId, int pageIndex, int pageSize)
    {
        return this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .Where(t => t.TagId == tagId)
            .OrderByDescending(t => t.Id)
            .WithPaging(pageIndex, pageSize);
    }

    /// <inheritdoc />
    public Task<PagedEntity<TaggedEntity>> LoadTaggedEntitiesAsync(int tagId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .Where(t => t.TagId == tagId)
            .OrderByDescending(t => t.Id)
            .WithPagingAsync(pageIndex, pageSize, cancellationToken);
    }

    /// <inheritdoc />
    public IEnumerable<TaggedEntity> LoadTaggedEntities(int tagId)
    {
        return this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .Where(t => t.TagId == tagId)
            .OrderByDescending(t => t.Id)
            .ToArray();
    }

    /// <inheritdoc />
    public Task<TaggedEntity[]> LoadTaggedEntitiesAsync(int tagId)
    {
        return this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .Where(t => t.TagId == tagId)
            .OrderByDescending(t => t.Id)
            .ToArrayAsync();
    }

    /// <inheritdoc />
    public string LoadTagNote(int taggedEntityId)
    {
        return this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .SingleOrDefault(a => a.Id == taggedEntityId)?.TagNote?.Note;
    }

    /// <inheritdoc />
    public async Task<string> LoadTagNoteAsync(int taggedEntityId, CancellationToken cancellationToken = default)
    {
        var tag = await this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .SingleOrDefaultAsync(a => a.Id == taggedEntityId, cancellationToken);

        return tag?.TagNote?.Note;
    }

    /// <inheritdoc />
    public string LoadTagNote(int tagId, string entityType, string entityKey)
    {
        return this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .SingleOrDefault(a => a.TagId == tagId && a.EntityType.Equals(entityType) && a.EntityKey.Equals(entityKey))?.TagNote?.Note;
    }

    /// <inheritdoc />
    public async Task<string> LoadTagNoteAsync(int tagId, string entityType, string entityKey, CancellationToken cancellationToken = default)
    {
        var tag = await this.dbContext.TaggedEntities
            .Include(a => a.TagNote)
            .SingleOrDefaultAsync(a => a.TagId == tagId && a.EntityType.Equals(entityType) && a.EntityKey.Equals(entityKey),
                cancellationToken: cancellationToken);

        return tag?.TagNote?.Note;
    }

    /// <inheritdoc />
    public PagedEntity<Tag> LoadTags(int pageIndex, int pageSize, params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.Id)
            .WithPaging(pageIndex, pageSize);
    }

    /// <inheritdoc />
    public Task<PagedEntity<Tag>> LoadTagsAsync(int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default,
        params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.Id)
            .WithPagingAsync(pageIndex, pageSize, cancellationToken);
    }

    /// <inheritdoc />
    public IEnumerable<Tag> LoadTags(params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.Id)
            .ToArray();
    }

    /// <inheritdoc />
    public Task<Tag[]> LoadTagsAsync(params TagOwnerFilter[] filters)
    {
        return this.dbContext.Tags
            .BelongingTo(filters.ToArray())
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.Id)
            .ToArrayAsync();
    }

    /// <inheritdoc />
    public void TagEntity(ICollection<int> tagIds, string entityType, string entityKey, string note, int userId)
    {
        var existingTagIds = this.dbContext.TaggedEntities
            .Where(t => t.EntityType == entityType && t.EntityKey == entityKey && tagIds.Contains(t.TagId))
            .Select(t => t.TagId)
            .ToList();

        var missingTagIds = tagIds.Except(existingTagIds).ToList();

        var tags = this.dbContext.Tags
            .Where(a => missingTagIds.Contains(a.Id))
            .ToArray();

        foreach (var tag in tags)
        {
            tag.TagEntity(entityKey, entityType, note, userId);
        }

        this.dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task TagEntityAsync(ICollection<int> tagIds,
        string entityType,
        string entityKey,
        string note,
        int userId,
        CancellationToken cancellationToken = default)
    {
        var existingTagIds = await this.dbContext.TaggedEntities
            .Where(t => t.EntityType == entityType && t.EntityKey == entityKey && tagIds.Contains(t.TagId))
            .Select(t => t.TagId)
            .ToListAsync(cancellationToken);

        var missingTagIds = tagIds.Except(existingTagIds).ToList();

        var tags = this.dbContext.Tags
            .Where(a => missingTagIds.Contains(a.Id))
            .AsAsyncEnumerable();

        await foreach (var tag in tags.WithCancellation(cancellationToken))
        {
            tag.TagEntity(entityKey, entityType, note, userId);
        }

        await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}