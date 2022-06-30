using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace mediafierWebApp.Models
{
    public partial class MediafierContext : DbContext
    {
        public MediafierContext()
        {
        }

        public MediafierContext(DbContextOptions<MediafierContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Folders> Folders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // Unable to generate entity type for table 'dbo.Table_1'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Table_bootcamp'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Mediafier;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.DocId);

                entity.ToTable("document");

                entity.Property(e => e.DocId).HasColumnName("doc_id");

                entity.Property(e => e.DocContentType)
                    .HasColumnName("doc_contentType")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocCreatedAt)
                    .HasColumnName("doc_createdAt")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.DocCreatedBy).HasColumnName("doc_createdBy");

                entity.Property(e => e.DocFolderId).HasColumnName("doc_folderId");

                entity.Property(e => e.DocIsDeleted).HasColumnName("doc_isDeleted");

                entity.Property(e => e.DocName)
                    .HasColumnName("doc_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocSize).HasColumnName("doc_size");

                entity.HasOne(d => d.DocCreatedByNavigation)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.DocCreatedBy)
                    .HasConstraintName("FK__document__doc_cr__6754599E");

                entity.HasOne(d => d.DocFolder)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.DocFolderId)
                    .HasConstraintName("FK__document__doc_fo__68487DD7");
            });

            modelBuilder.Entity<Folders>(entity =>
            {
                entity.Property(e => e.FoldersId).HasColumnName("folders_Id");

                entity.Property(e => e.FoldersCreatedAt)
                    .HasColumnName("folders_CreatedAt")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.FoldersCreatedBy).HasColumnName("folders_CreatedBy");

                entity.Property(e => e.FoldersIsdeleted)
                    .HasColumnName("folders_ISDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FoldersName)
                    .IsRequired()
                    .HasColumnName("folders_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FoldersCreatedByNavigation)
                    .WithMany(p => p.Folders)
                    .HasForeignKey(d => d.FoldersCreatedBy)
                    .HasConstraintName("FK__Folders__folders__5EBF139D");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UsersId).HasColumnName("users_Id");

                entity.Property(e => e.UsersCreatedAt)
                    .HasColumnName("users_CreatedAt")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.UsersPassword)
                    .IsRequired()
                    .HasColumnName("Users_Password")
                    .HasMaxLength(30);

                entity.Property(e => e.UsersUsername)
                    .IsRequired()
                    .HasColumnName("users_Username")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}
