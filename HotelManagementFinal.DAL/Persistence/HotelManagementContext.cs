using Microsoft.EntityFrameworkCore;
using Action = hotelManagement.DAL.Persistence.Entities.Action;
using hotelManagament.DAL.Persistence.Entities;
namespace hotelManagement.DAL.Persistence;

using hotelManagement.DAL.Persistence.Entities;

public partial class HotelManagementContext : DbContext
{
    public HotelManagementContext()
    {
    }

    public HotelManagementContext(DbContextOptions<HotelManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Akomodim> Akomodims { get; set; }

    public virtual DbSet<Dhome> Dhomes { get; set; }

    public virtual DbSet<ExtraService> ExtraServices { get; set; }

    public virtual DbSet<Fature> Fatures { get; set; }

    public virtual DbSet<Pagese> Pageses { get; set; }

    public virtual DbSet<Privilegj> Privilegjs { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Rezervim> Rezervims { get; set; }

    public virtual DbSet<RezervimService> RezervimServices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoomRate> RoomRates { get; set; }

    public virtual DbSet<RoomRateRange> RoomRateRanges { get; set; }

    public virtual DbSet<TipDhome> TipDhomes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-K6CTF30,1433;Database=HotelManagement;User ID=sa;Password=kleaklea2003;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Action__3213E83FEB425287");

            entity.ToTable("Action");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action1)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Pershkrim)
                .HasMaxLength(255)
                .HasColumnName("pershkrim");
        });

        modelBuilder.Entity<Akomodim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Akomodim__3213E83FBE20F82A");

            entity.ToTable("Akomodim");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adults).HasColumnName("adults");
            entity.Property(e => e.Cmim)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cmim");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Emer)
                .HasMaxLength(50)
                .HasColumnName("emer");
            entity.Property(e => e.Femije).HasColumnName("femije");
            entity.Property(e => e.KrevatExtra).HasColumnName("krevat_extra");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Dhome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dhome__3213E83F96B48DAD");

            entity.ToTable("Dhome");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Kat).HasColumnName("kat");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.NumerDhome)
                .HasMaxLength(10)
                .HasColumnName("numer_dhome");
            entity.Property(e => e.TipDhome).HasColumnName("tip_dhome");

            entity.HasOne(d => d.TipDhomeNavigation).WithMany(p => p.Dhomes)
                .HasForeignKey(d => d.TipDhome)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dhome__tip_dhome__47DBAE45");
        });

        modelBuilder.Entity<ExtraService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Extra_Se__3213E83F639C55E6");

            entity.ToTable("Extra_Service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Emer)
                .HasMaxLength(50)
                .HasColumnName("emer");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Pershkrim)
                .HasMaxLength(255)
                .HasColumnName("pershkrim");
        });

        modelBuilder.Entity<Fature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fature__3213E83F3C1E3DAF");

            entity.ToTable("Fature");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateFature).HasColumnName("date_fature");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Rezervim).HasColumnName("rezervim");
            entity.Property(e => e.RoomCharge)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("room_charge");
            entity.Property(e => e.ServiceCharge)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("service_charge");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.RezervimNavigation).WithMany(p => p.Fatures)
                .HasForeignKey(d => d.Rezervim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fature__rezervim__5BE2A6F2");
        });

        modelBuilder.Entity<Pagese>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagese__3213E83F96073782");

            entity.ToTable("Pagese");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DatePagese).HasColumnName("date_pagese");
            entity.Property(e => e.Fature).HasColumnName("fature");
            entity.Property(e => e.Menyre)
                .HasMaxLength(50)
                .HasColumnName("menyre");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.FatureNavigation).WithMany(p => p.Pageses)
                .HasForeignKey(d => d.Fature)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagese__fature__5EBF139D");
        });

        modelBuilder.Entity<Privilegj>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Privileg__3213E83F42893A8F");

            entity.ToTable("Privilegj");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Rol).HasColumnName("rol");

            entity.HasOne(d => d.ActionNavigation).WithMany(p => p.Privilegjs)
                .HasForeignKey(d => d.Action)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Privilegj__actio__3E52440B");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Privilegjs)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Privilegj__rol__3F466844");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Review__3213E83F7451A1D8");

            entity.ToTable("Review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Pershkrim)
                .HasMaxLength(255)
                .HasColumnName("pershkrim");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__user__4222D4EF");
        });

        modelBuilder.Entity<Rezervim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rezervim__3213E83FAA944CD7");

            entity.ToTable("Rezervim");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Akomodim).HasColumnName("akomodim");
            entity.Property(e => e.CheckIn).HasColumnName("check_in");
            entity.Property(e => e.CheckOut).HasColumnName("check_out");
            entity.Property(e => e.Cmim)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cmim");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dhome).HasColumnName("dhome");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.RoomRate).HasColumnName("room_rate");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.AkomodimNavigation).WithMany(p => p.Rezervims)
                .HasForeignKey(d => d.Akomodim)
                .HasConstraintName("FK__Rezervim__akomod__5441852A");

            entity.HasOne(d => d.DhomeNavigation).WithMany(p => p.Rezervims)
                .HasForeignKey(d => d.Dhome)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rezervim__dhome__52593CB8");

            entity.HasOne(d => d.RoomRateNavigation).WithMany(p => p.Rezervims)
                .HasForeignKey(d => d.RoomRate)
                .HasConstraintName("FK__Rezervim__room_r__534D60F1");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Rezervims)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rezervim__user__5165187F");
        });

        modelBuilder.Entity<RezervimService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rezervim__3213E83FFD6448BD");

            entity.ToTable("Rezervim_Service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Rezervim).HasColumnName("rezervim");
            entity.Property(e => e.Sasi).HasColumnName("sasi");
            entity.Property(e => e.Sherbim).HasColumnName("sherbim");

            entity.HasOne(d => d.RezervimNavigation).WithMany(p => p.RezervimServices)
                .HasForeignKey(d => d.Rezervim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rezervim___rezer__571DF1D5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FCED5E350");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EmerRoli)
                .HasMaxLength(50)
                .HasColumnName("emer_roli");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoomRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room_Rat__3213E83F91E26653");

            entity.ToTable("Room_Rate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CmimBaze)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cmim_baze");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Emer)
                .HasMaxLength(50)
                .HasColumnName("emer");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.TipDhomeId).HasColumnName("Tip_dhome_Id");

            entity.HasOne(d => d.TipDhome).WithMany(p => p.RoomRates)
                .HasForeignKey(d => d.TipDhomeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Room_Rate_Tip_dhome");
        });

        modelBuilder.Entity<RoomRateRange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room_Rat__3213E83FFDC12528");

            entity.ToTable("Room_Rate_Ranges");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.HolidayPricing)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("holiday_pricing");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.RoomRateId).HasColumnName("room_rate_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.WeekendPricing)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weekend_pricing");

            entity.HasOne(d => d.RoomRate).WithMany(p => p.RoomRateRanges)
                .HasForeignKey(d => d.RoomRateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room_Rate__room___4E88ABD4");
        });

        modelBuilder.Entity<TipDhome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tip_dhom__3213E83FA359572E");

            entity.ToTable("Tip_dhome");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Emer)
                .HasMaxLength(50)
                .HasColumnName("emer");
            entity.Property(e => e.Kapacitet).HasColumnName("kapacitet");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Pershkrim)
                .HasMaxLength(255)
                .HasColumnName("pershkrim");
            entity.Property(e => e.Siperfaqe)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("siperfaqe");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F2E2793ED");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E6164AF091484").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Emer)
                .HasMaxLength(50)
                .HasColumnName("emer");
            entity.Property(e => e.Mbiemer)
                .HasMaxLength(50)
                .HasColumnName("mbiemer");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
