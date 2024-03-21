using Microsoft.EntityFrameworkCore;
using MVCCRUDMODEL.Models.Domain;

namespace MVCCRUDMODEL.Data
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees {  get; set; } 
    }
}
