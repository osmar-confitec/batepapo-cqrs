using BatePapo.Data.Mappings;
using BatePapo.Domain.Models;
using BatePapo.DomainCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.Data.Context
{
   public class BatePapoContext : DbContext
    {
        public BatePapoContext(DbContextOptions<BatePapoContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

  



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              modelBuilder.ApplyConfiguration(new AdressMapping());
            modelBuilder.ApplyConfiguration(new CustomerMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
