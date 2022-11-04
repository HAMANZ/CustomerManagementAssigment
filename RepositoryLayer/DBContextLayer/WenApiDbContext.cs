using System;
using Microsoft.EntityFrameworkCore;
namespace  CustomerManagment.DomainLayer.Models
{
    public partial class WenApiDbContext : DbContext
    {
        public WenApiDbContext()
        {
        }

        public WenApiDbContext(DbContextOptions<WenApiDbContext> options) : base(options)
        {
                    
        }
        //Add-Migration Intial_Migration
        //Update-database
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<Country> Country { get; set; }
    
     
    }
}
