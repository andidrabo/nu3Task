using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace nu3Task.Entities
{
    public partial class nu3Context : DbContext
    {
        public nu3Context()
        {
        }

        public nu3Context(DbContextOptions<nu3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WINDOWS-SDH0A08;Database=nu3;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created-at");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.ProductId).HasColumnName("product-id");

                entity.Property(e => e.Src)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("src");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated-at");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Handle)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("handle");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("location");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BodyHtml)
                    .IsRequired()
                    .HasColumnName("body-html");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created-at");

                entity.Property(e => e.Handle)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("handle");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.ProductType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("product-type");

                entity.Property(e => e.PublishedScope)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("published-scope");

                entity.Property(e => e.Tags)
                    .IsRequired()
                    .HasColumnName("tags");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");

                entity.Property(e => e.Vendor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("vendor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
