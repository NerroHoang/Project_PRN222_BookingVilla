using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessObject;

public partial class BookingVillaPrnContext : DbContext
{
    public BookingVillaPrnContext()
    {
    }

    public BookingVillaPrnContext(DbContextOptions<BookingVillaPrnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AddService> AddServices { get; set; }

    public virtual DbSet<BookingOnline> BookingOnlines { get; set; }

    public virtual DbSet<CancelBooking> CancelBookings { get; set; }

    public virtual DbSet<ContactMessage> ContactMessages { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<ImageVilla> ImageVillas { get; set; }

    public virtual DbSet<PriceVilla> PriceVillas { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Villa> Villas { get; set; }
    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:BookingVillaPRN"];
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IdAccount).HasName("PK__Account__B7B00CE3BFE4938F");

            entity.ToTable("Account");

            entity.Property(e => e.PassWord)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AddService>(entity =>
        {
            entity.HasKey(e => new { e.IdBookingOnline, e.IdService }).HasName("PK__AddServi__B896CAA35CCF0BE3");

            entity.ToTable("AddService");

            entity.HasOne(d => d.IdBookingOnlineNavigation).WithMany(p => p.AddServices)
                .HasForeignKey(d => d.IdBookingOnline)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AddServic__IdBoo__5629CD9C");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.AddServices)
                .HasForeignKey(d => d.IdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AddServic__IdSer__571DF1D5");
        });

        modelBuilder.Entity<BookingOnline>(entity =>
        {
            entity.HasKey(e => e.IdBookingOnline).HasName("PK__BookingO__ACE21743360CACBF");

            entity.ToTable("BookingOnline");

            entity.Property(e => e.PriceBooking).HasColumnType("decimal(15, 0)");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.BookingOnlines)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK__BookingOn__IdCus__5812160E");

            entity.HasOne(d => d.IdVillaNavigation).WithMany(p => p.BookingOnlines)
                .HasForeignKey(d => d.IdVilla)
                .HasConstraintName("FK__BookingOn__IdVil__59063A47");
        });

        modelBuilder.Entity<CancelBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CancelBo__3213E83FCF085FCF");

            entity.ToTable("CancelBooking");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RequestDate).HasColumnName("requestDate");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("status");

            entity.HasOne(d => d.IdBookingOnlineNavigation).WithMany(p => p.CancelBookings)
                .HasForeignKey(d => d.IdBookingOnline)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CancelBoo__IdBoo__59FA5E80");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.CancelBookings)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CancelBoo__IdCus__5AEE82B9");
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ContactMessage");

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(200)
                .IsFixedLength();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__Customer__DB43864A78235841");

            entity.ToTable("Customer");

            entity.Property(e => e.IdCustomer).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Avatar)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCustomerNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__IdCust__5BE2A6F2");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.IdDiscount).HasName("PK__Discount__C6A0EA321C670DE6");

            entity.ToTable("Discount");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Percents).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK__Discount__IdAcco__5CD6CB2B");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__51C8DD7A19770B47");

            entity.ToTable("Employee");

            entity.Property(e => e.IdEmployee).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Avatar)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(15, 0)");

            entity.HasOne(d => d.IdEmployeeNavigation).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__IdEmpl__5DCAEF64");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.IdFeedback).HasName("PK__Feedback__408FF1037047B7C5");

            entity.ToTable("Feedback");

            entity.Property(e => e.ContentFeedback).HasMaxLength(200);

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK__Feedback__IdAcco__5EBF139D");

            entity.HasOne(d => d.IdVillaNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.IdVilla)
                .HasConstraintName("FK__Feedback__IdVill__5FB337D6");
        });

        modelBuilder.Entity<ImageVilla>(entity =>
        {
            entity.HasKey(e => e.IdImgVilla).HasName("PK__ImageVil__6E99CE4952F8DF16");

            entity.ToTable("ImageVilla");

            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .HasColumnName("image");

            entity.HasOne(d => d.IdVillaNavigation).WithMany(p => p.ImageVillas)
                .HasForeignKey(d => d.IdVilla)
                .HasConstraintName("FK__ImageVill__IdVil__60A75C0F");
        });

        modelBuilder.Entity<PriceVilla>(entity =>
        {
            entity.HasKey(e => e.IdPriceVilla).HasName("PK__PriceVil__BE46DA90BF3E396E");

            entity.ToTable("PriceVilla");

            entity.Property(e => e.PriceDay)
                .HasColumnType("decimal(15, 0)")
                .HasColumnName("Price_Day");

            entity.HasOne(d => d.IdVillaNavigation).WithMany(p => p.PriceVillas)
                .HasForeignKey(d => d.IdVilla)
                .HasConstraintName("FK__PriceVill__IdVil__6477ECF3");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PK__Service__474DDE008C9B0F37");

            entity.ToTable("Service");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.IdTransactions).HasName("PK__Transact__18013B81E51F4B0D");

            entity.Property(e => e.IdTransactions).ValueGeneratedNever();
            entity.Property(e => e.Price).HasColumnType("decimal(15, 0)");

            entity.HasOne(d => d.IdTransactionsNavigation).WithOne(p => p.Transaction)
                .HasForeignKey<Transaction>(d => d.IdTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__IdTra__66603565");
        });

        modelBuilder.Entity<Villa>(entity =>
        {
            entity.HasKey(e => e.IdVilla).HasName("PK__Villa__5755E0F0BBE59EAA");

            entity.ToTable("Villa");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Point).HasColumnType("decimal(5, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
