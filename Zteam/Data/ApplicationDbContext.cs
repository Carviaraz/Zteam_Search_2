using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zteam.Models;

namespace Zteam.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Game { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<BuyDtl> BuyDtl { get; set; }
        public DbSet<Buying> Buyings { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDtl> CartDtls { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Duty> Duty { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public object CartDtl { get; internal set; }
        public DbSet<SalesReport> SalesReports { get; set; }

    }

}
