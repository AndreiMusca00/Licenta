using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;

namespace BookingAPI.Data
{
    public class BookingAPIContext : DbContext
    {
        public BookingAPIContext (DbContextOptions<BookingAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BookingAPI.Models.Users> Users { get; set; }
        public DbSet<BookingAPI.Models.User> User { get; set; }
        public DbSet<BookingAPI.Models.Proprietate> Proprietate { get; set; }
        public DbSet<BookingAPI.Models.Rezervare> Rezervare { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(user => user.Role)
                .HasDefaultValue("Basic");
        }
    }
}
