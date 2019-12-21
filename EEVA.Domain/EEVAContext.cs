using System;
using System.Collections.Generic;
using System.Text;
using EEVA.Domain.models;
using Microsoft.EntityFrameworkCore;

namespace EEVA.Domain
{
    public class EEVAContext : DbContext
    {
        public EEVAContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(new Contact
            {
                Id = 1,
                FirstName = "Uncle",
                LastName = "Bob",
                Email = "uncle.bob@gmail.com",
                PhoneNumber = "999-888-7777"

            }, new Contact
            {
                Id = 2,
                FirstName = "Jan",
                LastName = "Kirsten",
                Email = "jan.kirsten@gmail.com",
                PhoneNumber = "111-222-3333"
            });
        }
    }
}
