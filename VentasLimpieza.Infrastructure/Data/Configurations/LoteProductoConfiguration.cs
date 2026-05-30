using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data.Configurations
{
    public class LoteProductoConfiguration : IEntityTypeConfiguration<Loteproducto>
    {
        public void Configure(EntityTypeBuilder<Loteproducto> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("loteproducto");
            entity.HasIndex(e => e.Activo, "IX_LoteProducto_Activo");
            entity.HasIndex(e => e.FechaCaducidad, "IX_LoteProducto_FechaCaducidad");
            entity.HasIndex(e => e.NumeroLote, "IX_LoteProducto_NumeroLote");
            entity.HasIndex(e => e.ProductoId, "IX_LoteProducto_ProductoId");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.NumeroLote).HasMaxLength(50);
            entity.Property(e => e.PrecioCompra).HasPrecision(10, 2);
            entity.Property(e => e.UbicacionAlmacen).HasMaxLength(50);

            entity.HasOne(d => d.Producto)
                .WithMany(p => p.Loteproductos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoteProducto_Producto");
        }
    }
}
