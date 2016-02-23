using System.Data.Entity;
using CopThis.Domain.Entities;

namespace CopThis.Data.EntityFramework
{
    public class CopContext : DbContext, IUnitOfWork
    {
        public CopContext() : base("CopContext")
        {
            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
