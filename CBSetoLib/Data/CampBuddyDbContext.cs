using System;
using System.Collections.Generic;
using System.Text;
using CBSetoLib.Models;
using CBSetoLib.Services;
using Microsoft.EntityFrameworkCore;

namespace CBSetoLib.Data
{
    public class CampBuddyDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }

        private readonly CampBuddyCharacterService _characterService;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(_characterService.GetCharacters());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cb.db");
        }
    }
}
