using System;
using System.Collections.Generic;
using DAL.EF.Table;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public partial class MedicineShopDbContext : DbContext
{
    public MedicineShopDbContext()
    {
    }

    public MedicineShopDbContext(DbContextOptions<MedicineShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseItem> PurchaseItems { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleItem> SaleItems { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.Describe)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.Property(e => e.MedicineId)
                .ValueGeneratedNever()
                .HasColumnName("Medicine_id");
            entity.Property(e => e.Brand)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.GenericName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_id");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medicines_Suppliers");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_id");
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Invoice_no");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_id");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Suppliers");
        });

        modelBuilder.Entity<PurchaseItem>(entity =>
        {
            entity.ToTable("Purchase_items");

            entity.Property(e => e.PurchaseItemId).HasColumnName("Purchase_item_id");
            entity.Property(e => e.MedicineId).HasColumnName("Medicine_id");
            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_id");
            entity.Property(e => e.UnitCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Medicine).WithMany(p => p.PurchaseItems)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchase_items_Medicines");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.SaleId).HasColumnName("Sale_id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Invoice_no");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.ToTable("Sale_items");

            entity.Property(e => e.SaleItemId).HasColumnName("Sale_item_id");
            entity.Property(e => e.MedicineId).HasColumnName("Medicine_id");
            entity.Property(e => e.SaleId).HasColumnName("Sale_id");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Medicine).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_items_Medicines");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.Property(e => e.StockId).HasColumnName("Stock_id");
            entity.Property(e => e.AlertType)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MedicineId).HasColumnName("Medicine_id");
            entity.Property(e => e.Message)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Medicine).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stocks_Medicines");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.InterestedOn)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("InterestedOn");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
