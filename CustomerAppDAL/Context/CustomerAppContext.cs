using CustomerAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppDAL.Context
{
    public class CustomerAppContext : DbContext
    {
        // 
        static DbContextOptions<CustomerAppContext> options =
            new DbContextOptionsBuilder<CustomerAppContext>()
            .UseInMemoryDatabase("TheDB")
            .Options;

        // Options that we want in memory.
        //public CustomerAppContext() : base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=CustomerApp;Trusted_Connection=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(ca => new { ca.AddressId, ca.CustomerId });

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Address)       // One Address
                .WithMany(a => a.Customers)     // With Many Customers
                .HasForeignKey(ca => ca.AddressId); // CustomerAddress and Customer bound with key

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Customer)
                .WithMany(a => a.Addresses)
                .HasForeignKey(ca => ca.CustomerId);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
