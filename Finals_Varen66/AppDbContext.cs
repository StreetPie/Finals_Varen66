using Microsoft.EntityFrameworkCore;
using Finals_Varen66.Models;
using System.Windows;

namespace Finals_Varen66.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Position> Positions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                "Server =DESKTOP-36VM1D0\\SQLEXPRESS; Database =Zachet; Trusted_Connection = True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}