#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace abcRetail.Models;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<InventoryTransaction> InventoryTransactions { get; set; }

    public virtual DbSet<MessagesLog> MessagesLogs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdminID).HasName("PK__Administ__719FE4E8ED0811E3");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerID).HasName("PK__Customer__A4AE64B870654BBE");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<InventoryTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionID).HasName("PK__Inventor__55433A4B1C98E493");

            entity.Property(e => e.TransactionDate).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Product).WithMany(p => p.InventoryTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Produ__6C190EBB");
        });

        modelBuilder.Entity<MessagesLog>(entity =>
        {
            entity.HasKey(e => e.MessageID).HasName("PK__Messages__C87C037C55419623");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Order).WithMany(p => p.MessagesLogs).HasConstraintName("FK__MessagesL__Order__6FE99F9F");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderID).HasName("PK__Orders__C3905BAF3E688861");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Customer__6477ECF3");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemID).HasName("PK__OrderIte__57ED06A16D94B880");

            entity.Property(e => e.Total).HasComputedColumnSql("([Quantity]*[UnitPrice])", true);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__68487DD7");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Produ__693CA210");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductID).HasName("PK__Products__B40CC6EDCE7D858B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}