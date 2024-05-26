using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions options) : base(options)
	{
	}

    public DbSet<Person>? People { get; set; }
}
