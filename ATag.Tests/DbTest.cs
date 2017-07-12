namespace ATag.Tests
{
    using ATag.EntityFrameworkCore;
    using Xunit;

    [Collection(nameof(DatabaseCollectionFixture))]
    public class DbTest
    {
        private readonly DatabaseFixture dbFixture;

        public DbTest(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact]
        public void CanCreateTag()
        {
            var repository = new TagRepository(this.dbFixture.CreateDataContext());
            var tag = repository.AddTag("Test", 1, "Role", 1);

            Assert.NotEqual(0, tag.Id);
            Assert.NotNull(repository.LoadTag(tag.Id));
        }
    }
}