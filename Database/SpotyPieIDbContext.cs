﻿using Microsoft.EntityFrameworkCore;
using Models.BackEnd;

namespace Database
{
    public class SpotyPieIDbContext : DbContext
    {
        public SpotyPieIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<CurrentSong> CurrentSong { get; set; }
        public DbSet<Playlist> Playlist { get; set; }

        public DbSet<Tracks> Tracks { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Copyright> Copyrights { get; set; }

        public DbSet<Item> ActiveSong { get; set; }
    }
}
