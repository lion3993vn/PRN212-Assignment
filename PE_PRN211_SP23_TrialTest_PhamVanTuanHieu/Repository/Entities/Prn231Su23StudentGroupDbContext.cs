using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.Entities;

public partial class Prn231Su23StudentGroupDbContext : DbContext
{
    public Prn231Su23StudentGroupDbContext()
    {
    }

    public Prn231Su23StudentGroupDbContext(DbContextOptions<Prn231Su23StudentGroupDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentGroup> StudentGroups { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC079D68E061");

            entity.ToTable("Student");

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.FullName).HasMaxLength(250);

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Student__GroupId__4E88ABD4");
        });

        modelBuilder.Entity<StudentGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentG__3214EC07EEC75729");

            entity.ToTable("StudentGroup");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.GroupName).HasMaxLength(250);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC07C8253BBB");

            entity.ToTable("UserRole");

            entity.HasIndex(e => e.Username, "UQ__UserRole__536C85E4E057E0E9").IsUnique();

            entity.Property(e => e.Passphrase)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.UserRole1).HasColumnName("UserRole");
            entity.Property(e => e.Username).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
