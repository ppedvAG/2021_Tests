using ppedv.Zugfahrt.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ppedv.Zugfahrt.Data.Ef
{
    public class EfContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Model.Zugfahrt> Fahrten { get; set; }

        public EfContext(string conString) : base(conString)
        { }

        public EfContext() : this("Server=(localdb)\\mssqllocaldb;Database=Zugfahrt_dev;Trusted_Connection=true")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Kunde>().HasMany<Ticket>(x => x.Tickets).WithRequired(x => x.Kunde).WillCascadeOnDelete(true);
        }

    }
}
