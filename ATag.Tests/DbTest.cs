﻿namespace ATag.Tests
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
		public void TagNotExists()
		{
			var teamFilter = new TagOwnerFilter(2, "Team");
			var personalFilter = new TagOwnerFilter(1, "1");

			var tags = this.tagRepository.LoadTags(1, 10, teamFilter, personalFilter).Results.ToList();

			Assert.Equal(0, tags.Count);
		}
	}
}