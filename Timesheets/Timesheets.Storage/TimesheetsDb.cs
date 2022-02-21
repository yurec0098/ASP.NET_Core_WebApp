#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Timesheets.Domain;

namespace Timesheets.Storage
{
	public class TimesheetsDb : DbContext
	{
		public TimesheetsDb(DbContextOptions<TimesheetsDb> options) : base(options)
		{
			Database.EnsureCreated();
		}


		public DbSet<User> User => Set<User>();
		public DbSet<Employee> Employee => Set<Employee>();
	}
}
