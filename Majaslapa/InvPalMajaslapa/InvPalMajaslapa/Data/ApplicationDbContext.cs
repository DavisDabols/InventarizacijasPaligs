using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvPalMajaslapa.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Logs> Logs { get; set; }
    }
}
