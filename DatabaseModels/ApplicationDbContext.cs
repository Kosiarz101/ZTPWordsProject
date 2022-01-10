using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.DatabaseModels
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordTranslation> WordTranslations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZTPWordsDatabase;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordTranslation>().HasKey(table => new {
                table.TranslationId,
                table.WordId
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
