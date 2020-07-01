using Microsoft.EntityFrameworkCore;
using RoomChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().Property(x => x.Guid).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<UserRoom>()
                .HasKey(t => new { t.Id, t.Guid });
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UserRoom> UserRooms { get; set; }
    }
}
