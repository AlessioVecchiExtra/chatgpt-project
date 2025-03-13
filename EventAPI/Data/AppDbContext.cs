using System;
using System.Collections.Generic;
using EventAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("User Id=sa;Password=Tgsd!ex06; Server=192.168.1.18;Database=Extrait_meeting_survey;Trusted_Connection=False;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.ToTable("Meeting");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Meeting).WithMany(p => p.Questions)
                .HasForeignKey(d => d.MeetingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Meeting");
        });

        modelBuilder.Entity<QuestionAnswer>(entity =>
        {
            entity.ToTable("QuestionAnswer");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionAnswer_Question");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.ToTable("Vote");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Meeting).WithMany(p => p.Votes)
                .HasForeignKey(d => d.MeetingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vote_Meeting");

            entity.HasOne(d => d.QuestionAnswer).WithMany(p => p.Votes)
                .HasForeignKey(d => d.QuestionAnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vote_QuestionAnswer");

            entity.HasOne(d => d.Question).WithMany(p => p.Votes)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vote_Question");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
