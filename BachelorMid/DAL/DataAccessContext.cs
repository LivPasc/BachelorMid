using BachelorMid.Models;
using Microsoft.EntityFrameworkCore;

namespace BachelorMid.DAL
{
    public class DataAccessContext : DbContext
    {
        public DbSet<ValuesModel> Values { get; set; }

        public DataAccessContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=10.152.220.93;Initial Catalog=MicrocontrollerDataDB;Persist Security Info=True;User ID=SA;Password=MSSQLPassword2020");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
