namespace ATag.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class DbEntityConfiguration<TEntity> where TEntity : class
	{
		public abstract void Configure(EntityTypeBuilder<TEntity> entity);
	}
}