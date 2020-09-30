using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WSVenta.Models
{
    public partial class VentaRealContext : DbContext
    {
        public VentaRealContext()
        {
        }

        public VentaRealContext(DbContextOptions<VentaRealContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Conceptos> Conceptos { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MSI;Database=VentaReal;User=test;Password=Ingeniero29;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__D5946642ECEDB9A7");

                entity.ToTable("Cliente", "Ventas");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Conceptos>(entity =>
            {
                entity.HasKey(e => e.IdConcepto)
                    .HasName("PK__Concepto__367401537F11E777");

                entity.ToTable("Conceptos", "Ventas");

                entity.Property(e => e.Importe).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(16, 2)");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Conceptos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conceptos_Producto");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.Conceptos)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conceptos__IdVen__398D8EEE");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__098892107B92533A");

                entity.ToTable("Producto", "Ventas");

                entity.Property(e => e.Costo).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.Nombre).HasMaxLength(64);

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(16, 2)");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__BC1240BDAFB495E4");

                entity.ToTable("Venta", "Ventas");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(16, 2)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Venta_Cliente");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
