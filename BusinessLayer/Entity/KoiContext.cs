using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Entity;

public partial class KoiContext : DbContext
{
    public KoiContext()
    {
    }

    public KoiContext(DbContextOptions<KoiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<FeedSchedule> FeedSchedules { get; set; }

    public virtual DbSet<KoiFish> KoiFishes { get; set; }

    public virtual DbSet<KoiGrowth> KoiGrowths { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Pond> Ponds { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<SaltCalculation> SaltCalculations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WaterParameter> WaterParameters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=KOI;User ID=sa;Password=12345;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.ProductId)
                .IsSparse()
                .HasColumnName("ProductID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Carts_Products");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Carts_Users");
        });

        modelBuilder.Entity<FeedSchedule>(entity =>
        {
            entity.HasKey(e => e.FeedId);

            entity.ToTable("FeedSchedule");

            entity.Property(e => e.FeedId).HasColumnName("FeedID");
            entity.Property(e => e.FeedAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FeedDate).HasColumnType("datetime");
            entity.Property(e => e.KoiId).HasColumnName("KoiID");
            entity.Property(e => e.Notes).HasMaxLength(255);

            entity.HasOne(d => d.KoiGrowthNavigation).WithMany(p => p.FeedSchedules)
                .HasForeignKey(d => d.KoiGrowth)
                .HasConstraintName("FK_FeedSchedule_KoiGrowth");

            entity.HasOne(d => d.Koi).WithMany(p => p.FeedSchedules)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK_FeedSchedule_KoiFish");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.KoiId);

            entity.ToTable("KoiFish");

            entity.Property(e => e.KoiId).HasColumnName("KoiID");
            entity.Property(e => e.Breed).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Origin).HasMaxLength(100);
            entity.Property(e => e.PondId).HasColumnName("PondID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Size).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Pond).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.PondId)
                .HasConstraintName("FK_KoiFish_Ponds1");
        });

        modelBuilder.Entity<KoiGrowth>(entity =>
        {
            entity.HasKey(e => e.GrowthId);

            entity.ToTable("KoiGrowth");

            entity.Property(e => e.GrowthId).HasColumnName("GrowthID");
            entity.Property(e => e.KoiId).HasColumnName("KoiID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Size).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Koi).WithMany(p => p.KoiGrowths)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK_KoiGrowth_KoiFish");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK_OrderItems_1");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Cart).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_OrderItems_Carts1");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderItems_Orders");
        });

        modelBuilder.Entity<Pond>(entity =>
        {
            entity.Property(e => e.PondId).HasColumnName("PondID");
            entity.Property(e => e.Depth).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PondName).HasMaxLength(100);
            entity.Property(e => e.PumpCapacity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Size).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Volume).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WaterDischargeRate).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Ponds)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Ponds_Users");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductDescription).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(100);
        });

        modelBuilder.Entity<SaltCalculation>(entity =>
        {
            entity.HasKey(e => e.SaltId);

            entity.ToTable("SaltCalculation");

            entity.Property(e => e.SaltId).HasColumnName("SaltID");
            entity.Property(e => e.CalculationDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.PondId).HasColumnName("PondID");
            entity.Property(e => e.SaltAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Pond).WithMany(p => p.SaltCalculations)
                .HasForeignKey(d => d.PondId)
                .HasConstraintName("FK_SaltCalculation_Ponds");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<WaterParameter>(entity =>
        {
            entity.HasKey(e => e.ParameterId);

            entity.Property(e => e.ParameterId).HasColumnName("ParameterID");
            entity.Property(e => e.MeasurementDate).HasColumnType("datetime");
            entity.Property(e => e.No2)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("NO2");
            entity.Property(e => e.No3)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("NO3");
            entity.Property(e => e.Oxygen).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PH)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("pH");
            entity.Property(e => e.Po4)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("PO4");
            entity.Property(e => e.PondId).HasColumnName("PondID");
            entity.Property(e => e.Salinity).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Temperature).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Pond).WithMany(p => p.WaterParameters)
                .HasForeignKey(d => d.PondId)
                .HasConstraintName("FK_WaterParameters_Ponds");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
