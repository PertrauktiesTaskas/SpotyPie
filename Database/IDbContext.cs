using Microsoft.EntityFrameworkCore;
using Models.BackEnd;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class IDbContext : DbContext
    {
        public IDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Tracks> Tracks { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Copyright> Copyrights { get; set; }
    }
}
