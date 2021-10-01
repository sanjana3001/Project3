using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Project3.Models
{
    public partial class ProjectContext : DbContext
    {
        public ProjectContext()
        {
        }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AvailableLaptop> AvailableLaptop { get; set; }
        public virtual DbSet<TblAdmin> TblAdmin { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<TblOrderedLaptop> TblOrderedLaptop { get; set; }
        public virtual DbSet<TblSeller> TblSeller { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=LAPTOP-5HS0QN24\\MSSQLSERVER01;integrated security=true;database=Project");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvailableLaptop>(entity =>
            {
                entity.HasKey(e => e.Lid)
                    .HasName("PK__availabl__C655574190147855");

                entity.ToTable("availableLaptop");

                entity.Property(e => e.Lid)
                    .HasColumnName("LId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Lmodel)
                    .HasColumnName("LModel")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .HasColumnName("LName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lprice).HasColumnName("LPrice");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.AvailableLaptop)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__available__Selle__31EC6D26");
            });

            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblAdmin");

                entity.Property(e => e.Acontact)
                    .HasColumnName("AContact")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Aemail)
                    .HasColumnName("AEmail")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Aid).HasColumnName("AId");

                entity.Property(e => e.Aname)
                    .HasColumnName("AName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Apass)
                    .HasColumnName("APass")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK__tblCusto__C1F8DC39A815FC86");

                entity.ToTable("tblCustomer");

                entity.Property(e => e.Cid)
                    .HasColumnName("CId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ccontact)
                    .HasColumnName("CContact")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cemail)
                    .HasColumnName("CEmail")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cname)
                    .HasColumnName("CName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cpass)
                    .HasColumnName("CPass")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblOrderedLaptop>(entity =>
            {
                entity.HasKey(e => e.Oid)
                    .HasName("PK__tblOrder__CB394B1963126A75");

                entity.ToTable("tblOrderedLaptop");

                entity.Property(e => e.Oid)
                    .HasColumnName("OId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Lid).HasColumnName("LId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblOrderedLaptop)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__tblOrdere__Custo__3D5E1FD2");

                entity.HasOne(d => d.L)
                    .WithMany(p => p.TblOrderedLaptop)
                    .HasForeignKey(d => d.Lid)
                    .HasConstraintName("FK__tblOrderedL__LId__3B75D760");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.TblOrderedLaptop)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__tblOrdere__Selle__3C69FB99");
            });

            modelBuilder.Entity<TblSeller>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__tblSelle__CA19595099BAA501");

                entity.ToTable("tblSeller");

                entity.Property(e => e.Sid)
                    .HasColumnName("SId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Scontact)
                    .HasColumnName("SContact")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Semail)
                    .HasColumnName("SEmail")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sname)
                    .HasColumnName("SName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Spass)
                    .HasColumnName("SPass")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
