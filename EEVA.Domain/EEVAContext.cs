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
    }
}
