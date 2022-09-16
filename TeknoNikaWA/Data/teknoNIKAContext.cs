using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;





namespace TeknoNikaWA.Data

{
    public partial class teknoNIKAContext : DbContext
    {
        public teknoNIKAContext()
        {
        }

        public teknoNIKAContext(DbContextOptions<teknoNIKAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<File> Files { get; set; } = null!;
        public virtual DbSet<LayoutType> LayoutTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductFile> ProductFiles { get; set; } = null!;
        public virtual DbSet<ProductSale> ProductSales { get; set; } = null!;
        public virtual DbSet<Quota> Quotas { get; set; } = null!;
        public virtual DbSet<SalesOrder> SalesOrders { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-S3GJTPT\\SQLEXPRESS;Database=teknoNIKA;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryName).HasMaxLength(20);

                entity.Property(e => e.Description).HasColumnType("ntext");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Firstname).HasMaxLength(20);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Lastname).HasMaxLength(20);

                entity.Property(e => e.TcId).HasColumnName("TcID");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.Lastname).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(40);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(20);

                entity.Property(e => e.FilePath).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<LayoutType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LayoutName).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<ProductFile>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.LayoutTypeId).HasColumnName("LayoutTypeID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.File)
                    .WithMany()
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK_ProductFiles_Files");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductFiles_LayoutTypes");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductFiles_Products");
            });

            modelBuilder.Entity<ProductSale>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductSales_Products");

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.SalesOrderId)
                    .HasConstraintName("FK_ProductSales_SalesOrder");
            });

            modelBuilder.Entity<Quota>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Quota)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Quotas_Employees");
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.ToTable("SalesOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.SaleDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_SalesOrder_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_SalesOrder_Employees");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CompanyName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
