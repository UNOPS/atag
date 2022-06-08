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
			this.tagRepository = new TagRepository(dbFixture.CreateDataContext());
		}

		private readonly ITagRepository tagRepository;

		[Fact]
		public void CanCreateTag()
		{
			var tagId = this.tagRepository.AddTag(new Tag("Test", 1, "1", 1));

			Assert.NotEqual(0, tagId);
			Assert.NotNull(this.tagRepository.LoadTag(tagId));
		}

		[Fact]
		public void CanOwnMultipleTag()
		{
			var tagId = this.tagRepository.AddTag(new Tag("Procure computer", 2, "Manager", 1));

			Assert.NotEqual(0, tagId);
			Assert.NotNull(this.tagRepository.LoadTag(tagId));

			var teamFilters = new[]
			{
				new TagOwnerFilter(2, "Manager"),
				new TagOwnerFilter(2, "Supervisor"),
				new TagOwnerFilter(1, "John")
			};

			var tags = this.tagRepository.LoadTags(teamFilters).ToList();

			Assert.NotEmpty(tags);
		}

		[Fact]
		public void CreateTaggedEntity()
		{
			var tagId = this.tagRepository.AddTag(new Tag("TestABC", 1, "1", 1));

			Assert.NotEqual(0, tagId);
			Assert.NotNull(this.tagRepository.LoadTag(tagId));

			var entityKey = "1";
			var entityType = "Circle";
			this.tagRepository.TagEntity(new[] { tagId }, entityType, entityKey, "Test ABC", 1);

			var data = this.tagRepository.LoadTaggedEntities(tagId, 1, 10);
			Assert.NotEmpty(data.Results.Select(a => a.EntityKey.Equals(entityKey) && a.EntityType.Equals(entityType)));
		}

		[Fact]
		public void LoadTagNoteWithFilters()
		{
			var testAbc = "Test ABC";
			var tagId = this.tagRepository.AddTag(new Tag(testAbc, 1, "1", 1));

			Assert.NotEqual(0, tagId);
			Assert.NotNull(this.tagRepository.LoadTag(tagId));

			var entityKey = "1";
			var entityType = "Watch";

			this.tagRepository.TagEntity(new[] { tagId }, entityType, entityKey, "Test Note", 1);

			var teamFilter = new TagOwnerFilter(2, "Team");
			var personalFilter = new TagOwnerFilter(1, "1");

			var tag = this.tagRepository.LoadTags(teamFilter, personalFilter).First(a => a.Name.Equals(testAbc));
			var note = this.tagRepository.LoadTagNote(tag.Id, entityType, entityKey);

			Assert.NotEmpty(note);
		}

		[Fact]
		public void TagNotExists()
		{
			var teamFilter = new TagOwnerFilter(2, "Team");
			var personalFilter = new TagOwnerFilter(1, "1");

			var tags = this.tagRepository.LoadTags(teamFilter, personalFilter).ToList();

			Assert.Equal(0, tags.Count);
		}
	}
}