using Microsoft.EntityFrameworkCore;

//create the database context for a particular model. This will be used to create a table (entity) for a particular model via the entity framework
namespace EmployeeApi.Models
{
    //employeecontext is an instance of dbcontext
    public class EmployeeContext : DbContext
    {
        //create an instance of dbcontext options generic of type EmployeeContext. hover over the functions fo indepth understanding
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
// {
//     // Configure StudentId as FK for StudentAddress
//     modelBuilder.Entity<EmployeeItem>().HasOne(e=>e.Department);

// }

        public DbSet<EmployeeItem> EmployeeItems { get; set; }
        public DbSet<DepartmentItem> DepartmentItems { get; set; }

    }
}