using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiviBank_Core.Models;

namespace DiviBank_Core.Models.Db
{
    public class DiviContext : DbContext
    {
        public DiviContext(DbContextOptions<DiviContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Client>()
                .HasKey(hk => hk.Id);

            modelBuilder.Entity<Client>()
                .HasMany(hm => hm.Loans).WithOne().HasForeignKey(fk => fk.Id);
            */
            modelBuilder.Entity<Loan>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,3)");
        }

        public DbSet<DiviBank_Core.Models.Client> Clients { get; set; }

        public DbSet<DiviBank_Core.Models.Loan> Loans { get; set; }
    }
}
