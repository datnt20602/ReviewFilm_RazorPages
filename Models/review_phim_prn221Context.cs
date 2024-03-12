using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Review_Film_Project.Models
{
    public partial class review_phim_prn221Context : DbContext
    {
        public review_phim_prn221Context()
        {
        }

        public review_phim_prn221Context(DbContextOptions<review_phim_prn221Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Reply> Replies { get; set; } = null!;
        public virtual DbSet<Url> Urls { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-210GMIE\\TIENDAT;Initial Catalog=review_phim_prn221;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__feedback__movie___398D8EEE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__feedback__user_i__38996AB5");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.AverageGrade).HasColumnName("average_grade");

                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .HasColumnName("description");

                entity.Property(e => e.Director)
                    .HasMaxLength(255)
                    .HasColumnName("director");

                entity.Property(e => e.Genre)
                    .HasMaxLength(255)
                    .HasColumnName("genre");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.MovieName)
                    .HasMaxLength(255)
                    .HasColumnName("movie_name");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("release_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__movies__user_id__300424B4");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.ToTable("reply");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.RepliedByUserId).HasColumnName("replied_by_user_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK__reply__feedback___412EB0B6");

                entity.HasOne(d => d.RepliedByUser)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.RepliedByUserId)
                    .HasConstraintName("FK__reply__replied_b__4222D4EF");
            });

            modelBuilder.Entity<Url>(entity =>
            {
                entity.ToTable("url");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.UrlValue)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("url_value");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Urls)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__url__movie_id__47DBAE45");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Urls)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__url__user_id__48CFD27E");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CodeActive)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("codeActive");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
