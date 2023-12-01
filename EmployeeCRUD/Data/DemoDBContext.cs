using EmployeeCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Data
{
	public class DemoDBContext : DbContext
	{
		public DemoDBContext(DbContextOptions options) : base(options)
		{
		}

        public DbSet<Employee> Employees { get; set; }
    }
}
