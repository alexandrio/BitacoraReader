
using BitacoraReader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BitacoraReader.Infrastructure.Data
{
    public class BitacoraDbContext : DbContext
    {
        public BitacoraDbContext(DbContextOptions<BitacoraDbContext> options) : base(options)
        {
        }
        public DbSet<Bitacora> Bitacoras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.IdBitacora);
                entity.Property(e => e.IdBitacora).ValueGeneratedOnAdd();
                entity.Property(e => e.NombreArchivo).HasMaxLength(1000);
                entity.Property(e => e.Resultado).HasMaxLength(100);
               // entity.Property(e => e.Hora).HasColumnType("time");
            });
        }
    }
}