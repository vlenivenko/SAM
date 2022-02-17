using Microsoft.EntityFrameworkCore;
using SAM.Repository.Models;

namespace SAM.Repository.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
