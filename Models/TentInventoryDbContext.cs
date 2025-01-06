using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TentBooking.Models;

public partial class TentInventoryDbContext : DbContext
{
    public TentInventoryDbContext()
    {
    }

    public TentInventoryDbContext(DbContextOptions<TentInventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Design> Designs { get; set; }

    public virtual DbSet<EmployeesTeam> EmployeesTeams { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SBINO-PC\\SQLEXPRESS;Database=TentInventoryDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951AED42E467DF");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingDate).HasColumnType("date");
            entity.Property(e => e.FunctionName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Design).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.DesignId)
                .HasConstraintName("FK__Booking__DesignI__59FA5E80");

            entity.HasOne(d => d.Product).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Booking__Product__59063A47");

            entity.HasOne(d => d.Team).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Booking__TeamId__5812160E");
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.ClaimsId).HasName("PK__Claims__7507312BA9E529EA");

            entity.Property(e => e.ClaimType).HasMaxLength(50);
            entity.Property(e => e.ClaimValue).HasMaxLength(150);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Client__E67E1A243B626DEF");

            entity.ToTable("Client");

            entity.Property(e => e.ClientBookingDate).HasColumnType("date");
            entity.Property(e => e.ClientName).HasMaxLength(100);
            entity.Property(e => e.ClientReturnDate).HasColumnType("date");
            entity.Property(e => e.ClientStatus).HasMaxLength(50);
            entity.Property(e => e.Contact1).HasMaxLength(15);
            entity.Property(e => e.Contact2).HasMaxLength(15);
            entity.Property(e => e.FunctionName).HasMaxLength(100);
            entity.Property(e => e.HomeAddress).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TentFunctionAddress).HasMaxLength(255);

            entity.HasOne(d => d.Booking).WithMany(p => p.Clients)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Client__BookingI__5CD6CB2B");
        });

        modelBuilder.Entity<Design>(entity =>
        {
            entity.HasKey(e => e.DesignId).HasName("PK__Design__32B8E15F52247B38");

            entity.ToTable("Design");

            entity.Property(e => e.DesignDescription).HasMaxLength(255);
            entity.Property(e => e.DesignImageUrl).HasMaxLength(255);
            entity.Property(e => e.DesignName).HasMaxLength(100);
        });

        modelBuilder.Entity<EmployeesTeam>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Employee__123AE79942965604");

            entity.ToTable("EmployeesTeam");

            entity.Property(e => e.BookingAddress).HasMaxLength(255);
            entity.Property(e => e.BookingEndDate).HasColumnType("date");
            entity.Property(e => e.BookingStartDate).HasColumnType("date");
            entity.Property(e => e.Etstatus)
                .HasMaxLength(50)
                .HasColumnName("ETStatus");
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TeamName).HasMaxLength(100);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6B325FB0B8D");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryDateOn).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Pstatus)
                .HasMaxLength(50)
                .HasColumnName("PStatus");
            entity.Property(e => e.UpdatedOn).HasColumnType("date");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Inventory__Produ__4D94879B");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDDA487F79");

            entity.ToTable("Product");

            entity.Property(e => e.BuyDate).HasColumnType("date");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.PriceRange)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductImageUrl).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.Quality).HasMaxLength(50);
            entity.Property(e => e.Totalstock).HasColumnName("totalstock");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AE528C1A9");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CABE3A616");

            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserClai__3213E83F491AD987");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Claim).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.ClaimId)
                .HasConstraintName("FK__UserClaim__Claim__634EBE90");

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserClaim__UserI__625A9A57");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3213E83F9385C6A5");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRoles__RoleI__671F4F74");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRoles__UserI__662B2B3B");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("PK__Worker__077C8826EE3FB941");

            entity.ToTable("Worker");

            entity.Property(e => e.Contact).HasMaxLength(15);
            entity.Property(e => e.JoiningDate).HasColumnType("date");
            entity.Property(e => e.Post).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WorkerAddress).HasMaxLength(255);
            entity.Property(e => e.WorkerName).HasMaxLength(100);
            entity.Property(e => e.WorkerStatus).HasMaxLength(50);

            entity.HasOne(d => d.Team).WithMany(p => p.Workers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Worker__TeamId__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
