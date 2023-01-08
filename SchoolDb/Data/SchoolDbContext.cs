using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolDb.Models;

namespace SchoolDb.Data
{
    public partial class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
        {
        }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<StaffRole> StaffRoles { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-1RE5FK4; Initial Catalog=SkolaDB; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.DateAdded).HasColumnType("date");

                entity.Property(e => e.FkStaffId).HasColumnName("FK_StaffId");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.Property(e => e.FkSubjectId).HasColumnName("FK_SubjectId");

                entity.Property(e => e.Grade1).HasColumnName("Grade");

                entity.HasOne(d => d.FkStaff)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkStaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__FK_StaffI__6A30C649");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__FK_Studen__693CA210");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__FK_Subjec__68487DD7");
            });

            modelBuilder.Entity<StaffRole>(entity =>
            {
                entity.ToTable("StaffRole");

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.FkClassId).HasColumnName("FK_ClassId");

                entity.Property(e => e.Fname)
                    .HasMaxLength(30)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(30)
                    .HasColumnName("LName");

                entity.Property(e => e.SocialNum)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FkClassId)
                    .HasConstraintName("FK__Student__FK_Clas__5FB337D6");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.EmpDate).HasColumnType("date");

                entity.Property(e => e.FkStaffRoleId).HasColumnName("FK_StaffRoleId");

                entity.Property(e => e.Fname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LName");

                entity.HasOne(d => d.FkStaffRole)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.FkStaffRoleId)
                    .HasConstraintName("FK__Staff__FK_StaffR__2D27B809");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
