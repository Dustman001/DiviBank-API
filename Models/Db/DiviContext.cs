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

            modelBuilder.Entity<Loans>()
                .HasMany(hm => hm.Loans).WithOne().HasForeignKey(fk => fk.Id);
            */
            modelBuilder.Entity<Client>()
                .HasMany(hm => hm.Loans).WithOne().HasForeignKey(fk => fk.ClientId);

            modelBuilder.Entity<Loan>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Client>()
                .HasData(new Client() { Id = 1, Name = "Ivan Padron", BirthDate = Convert.ToDateTime("1984/08/23"), Contact = "+18097475644" });

            modelBuilder.Entity<Loan>()
                .HasData(new Loan() { Id = 1, Amount = 1500, Date = DateTime.Now, ClientId = 1 });
        }

        public DbSet<DiviBank_Core.Models.Client> Clients { get; set; }

        public DbSet<DiviBank_Core.Models.Loan> Loans { get; set; }
    }
}
