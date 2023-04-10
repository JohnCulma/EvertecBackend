using System;
using System.Collections.Generic;
using Data.Models.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace EvertecApi.Data
{
    public partial class EvertecContext : DbContext
    {
        public EvertecContext()
        {
        }

        public EvertecContext(DbContextOptions<EvertecContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuarios> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Evertec");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("Usuarios", "evertec");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Apellidos).HasMaxLength(100);

                entity.Property(e => e.EmailUsuarioRegistro).HasMaxLength(1);

                entity.Property(e => e.FechaDeNacimiento).HasColumnType("datetime");

                entity.Property(e => e.FechaModifico).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombres).HasMaxLength(100);

                entity.Property(e => e.UsuarioModifico).HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
