using Microsoft.EntityFrameworkCore;
using EventAPI.Models;

namespace EventAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Vote> Votes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>()
              .HasKey(v => v.Id); // Definizione chiave primaria

            modelBuilder.Entity<Question>()
                .HasKey(v => v.Id); // Definizione chiave primaria.

            modelBuilder.Entity<QuestionAnswer>()
                .HasKey(v => v.Id); // Definizione chiave primaria.

            modelBuilder.Entity<Vote>()
                .HasKey(v => v.Id); // Definizione chiave primaria

            base.OnModelCreating(modelBuilder);
        }
    }
}
