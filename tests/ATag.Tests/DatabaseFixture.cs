namespace ATag.Tests;

using ATag.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DatabaseFixture
{
	private readonly DbContextOptions options;

	public DatabaseFixture()
	{
		this.options = new DbContextOptionsBuilder().UseInMemoryDatabase("Db").Options;
	}

	public DataContext CreateDataContext()
	{
		return new DataContext(this.options);
	}
}