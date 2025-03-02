﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using hotelManagement.DAL.Persistence.Entities;
using Action = hotelManagement.DAL.Persistence.Entities.Action;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting.Server;
namespace hotelManagement.DAL.Persistence;

public partial class HotelManagementContext : DbContext
{
    private readonly IConfiguration _configuration;

    public HotelManagementContext(DbContextOptions<HotelManagementContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

 

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Dhome> Dhomes { get; set; }

    public virtual DbSet<ExtraService> ExtraServices { get; set; }

    public virtual DbSet<Fature> Fatures { get; set; }

    public virtual DbSet<Pagese> Pageses { get; set; }

    public virtual DbSet<Privilegj> Privilegjs { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Rezervim> Rezervime { get; set; }

    public virtual DbSet<RezervimService> RezervimServices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoomRate> RoomRates { get; set; }

    public virtual DbSet<RoomRateRangeDataAccess> RoomRateRanges { get; set; }

    public virtual DbSet<TipDhome> TipDhomes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-F8EDG2P;Database=HotelManagement;User ID=sa;Password=Ester123;Trusted_Connection=False;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Action__3213E83F3AC4001F");

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

       

        modelBuilder.Entity<Dhome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dhome__3213E83F1285888A");

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
            entity.HasKey(e => e.Id).HasName("PK__Extra_Se__3213E83FD56C0A74");

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
            entity.HasKey(e => e.Id).HasName("PK__Fature__3213E83FE20FB342");

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
            entity.HasKey(e => e.Id).HasName("PK__Pagese__3213E83F98F9EDBA");

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
            entity.HasKey(e => e.Id).HasName("PK__Privileg__3213E83FDDFA1DFB");

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
            entity.HasKey(e => e.Id).HasName("PK__Review__3213E83FD6859F07");

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
            entity.HasKey(e => e.Id).HasName("PK__Rezervim__3213E83F27743923");

            entity.ToTable("Rezervim");

            entity.Property(e => e.Id).HasColumnName("id");
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


            entity.HasOne(d => d.DhomeNavigation).WithMany(p => p.Rezervims)
                .HasForeignKey(d => d.Dhome)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rezervim__dhome__52593CB8");

            entity.HasOne(d => d.RoomRateNavigation).WithMany(p => p.Rezervims)
                .HasForeignKey(d => d.RoomRate)
                .HasConstraintName("FK__Rezervim__room_r__534D60F1");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Rezervime)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rezervim__user__5165187F");
        });

        modelBuilder.Entity<RezervimService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rezervim__3213E83F06C4BE99");

            entity.ToTable("Rezervim_Service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Rezervim).HasColumnName("rezervim");
            entity.Property(e => e.Sherbim).HasColumnName("sherbim");
            entity.Property(e => e.Price).HasColumnName("Cmim");

            entity.HasOne(d => d.RezervimNavigation).WithMany(p => p.RezervimServices)
                .HasForeignKey(d => d.Rezervim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rezervim___rezer__571DF1D5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FC7E12958");

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
            entity.HasKey(e => e.Id).HasName("PK__Room_Rat__3213E83F7D77E30B");

            entity.ToTable("Room_Rate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RateMultiplier)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("rate_multiplier");
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

        modelBuilder.Entity<RoomRateRangeDataAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room_Rat__3213E83F9269A2A3");

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

         
        });

        modelBuilder.Entity<TipDhome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tip_dhom__3213E83FBF176496");

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
            entity.Property(e => e.CmimBaze)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("cmim_baze");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F27E98752");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E616432F7D37A").IsUnique();

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
