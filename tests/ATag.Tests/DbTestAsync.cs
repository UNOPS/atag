namespace ATag.Tests;

using System.Linq;
using ATag.Core;
using ATag.EntityFrameworkCore;
using Xunit;

[Collection(nameof(DatabaseCollectionFixture))]
public class DbTestAsync
{
    public DbTestAsync(DatabaseFixture dbFixture)
    {
        this.tagRepository = new TagRepository(dbFixture.CreateDataContext());
    }

    private readonly ITagRepository tagRepository;

    [Fact]
    public async Task CanCreateTagAsync()
    {
        var tagId = await this.tagRepository.AddTagAsync(new Tag("Test (Async)", 2, "2", 2));

        Assert.NotEqual(0, tagId);
        Assert.NotNull(await this.tagRepository.LoadTagAsync(tagId));
    }

    [Fact]
    public async Task CanOwnMultipleTagAsync()
    {
        var tagId = await this.tagRepository.AddTagAsync(new Tag("Procure computer (Async)", 2, "Manager (Async)", 1));

        Assert.NotEqual(0, tagId);
        Assert.NotNull(await this.tagRepository.LoadTagAsync(tagId));

        var teamFilters = new[]
        {
            new TagOwnerFilter(2, "Manager (Async)"),
            new TagOwnerFilter(2, "Supervisor (Async)"),
            new TagOwnerFilter(1, "John (Async)")
        };

        var tags = (await this.tagRepository.LoadTagsAsync(teamFilters)).ToList();

        Assert.NotEmpty(tags);
    }

    [Fact]
    public async Task CreateTaggedEntityAsync()
    {
        var tagId = await this.tagRepository.AddTagAsync(new Tag("TestABC (Async)", 1, "1", 1));

        Assert.NotEqual(0, tagId);
        Assert.NotNull(await this.tagRepository.LoadTagAsync(tagId));

        var (entityKey, entityType) = ("1", "Circle");

        await this.tagRepository.TagEntityAsync(new[] { tagId }, entityType, entityKey, "Test ABC (Async)", 1);

        var data = await this.tagRepository.LoadTaggedEntitiesAsync(tagId, 1, 10);
        Assert.NotEmpty(data.Results.Select(a => a.EntityKey.Equals(entityKey) && a.EntityType.Equals(entityType)));
    }

    [Fact]
    public async Task LoadTagNoteWithFiltersAsync()
    {
        var testAbc = "Test ABC";
        var tagId = await this.tagRepository.AddTagAsync(new Tag(testAbc, 1, "1", 1));

        Assert.NotEqual(0, tagId);
        Assert.NotNull(await this.tagRepository.LoadTagAsync(tagId));

        var (entityKey, entityType) = ("1", "Watch");

        await this.tagRepository.TagEntityAsync(new[] { tagId }, entityType, entityKey, "Test Note (Async)", 1);

        var teamFilter = new TagOwnerFilter(2, "Team (Async)");
        var personalFilter = new TagOwnerFilter(1, "1");

        var tag = this.tagRepository.LoadTags(teamFilter, personalFilter).First(a => a.Name.Equals(testAbc));
        var note = await this.tagRepository.LoadTagNoteAsync(tag.Id, entityType, entityKey);

        Assert.NotEmpty(note);
    }

    [Fact]
    public async Task TagNotExistsAsync()
    {
        var teamFilter = new TagOwnerFilter(2, "Team (Async)");
        var personalFilter = new TagOwnerFilter(2, "2");

        var tags = (await this.tagRepository.LoadTagsAsync(teamFilter, personalFilter)).ToList();

        Assert.Empty(tags);
    }
}