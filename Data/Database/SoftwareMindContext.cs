using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Database
{
    public class SoftwareMindContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public SoftwareMindContext(DbContextOptions<SoftwareMindContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=SoftwareMind;TrustServerCertificate=True;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var defaultDepartments = new Department[]
            {
                new Department { DepartmentId = -1, Name = "Onboarding" },
                new Department { DepartmentId = 1, Name = "Human Resources" },
                new Department { DepartmentId = 2, Name = "Finance" },
                new Department { DepartmentId = 3, Name = "IT" },
                new Department { DepartmentId = 4, Name = "Desktop Support" },
                new Department { DepartmentId = 5, Name = "Help Desk" },
                new Department { DepartmentId = 6, Name = "Software Engineering" },
                new Department { DepartmentId = 7, Name = "Management" },
            };
            modelBuilder.Entity<Department>().HasData(defaultDepartments);

            modelBuilder.Entity<Employee>().HasData(
                new Employee {
                    FirstName = "John", LastName = "Goodguy", Address = "Street 123", Phone = 9013214444, HireDate = new DateTime(2024, 8, 1, 14, 14, 14), DepartmentId = 1, EmployeeId = 1
                });
        }
    }
}
