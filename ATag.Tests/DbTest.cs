namespace ATag.Tests
{
    using System.Linq;
    using ATag.Core;
    using ATag.EntityFrameworkCore;
    using Xunit;

    [Collection(nameof(DatabaseCollectionFixture))]
    public class DbTest
    {
        public DbTest(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
            this.tagRepository = new TagRepository(this.dbFixture.CreateDataContext());
        }

        private readonly DatabaseFixture dbFixture;
        private readonly TagRepository tagRepository;

        [Fact]
        public void CanCreateTag()
        {
            var tagService = new TagService(this.tagRepository);

            var tagId = tagService.AddTag("Test", "1", 1, 1);

            Assert.NotEqual(0, tagId);
            Assert.NotNull(this.tagRepository.LoadTag(tagId));
        }

        [Fact]
        public void CreateTaggedEntity()
        {
            var tagService = new TagService(this.tagRepository);

            var tagId = tagService.AddTag("TestABC", "1", 1, 1);

            Assert.NotEqual(0, tagId);
            Assert.NotNull(this.tagRepository.LoadTag(tagId));

            var entityKey = "1";
            var entityType = "Circle";
            tagService.TagEntity(new[] { tagId }, entityType, entityKey, "Test ABC", 1);

            var data = tagService.LoadTagEntities(tagId, 1, 10);
            Assert.NotEmpty(data.Results.Select(a => a.EntityKey.Equals(entityKey) && a.EntityType.Equals(entityType)));
        }

        [Fact]
        public void TagNotExists()
        {
            var tagService = new TagService(this.tagRepository);

            var teamFilter = new TagOwnerFilter(2, "Team");
            var personalFilter = new TagOwnerFilter(1, "1");

            var tags = tagService.LoadTags(1, 10, teamFilter, personalFilter).Results.ToList();

            Assert.Equal(0, tags.Count);
        }
    }
}