using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data.Configurations
{
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<Detallepedido>
    {
        public void Configure(EntityTypeBuilder<Detallepedido> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("detallepedido");
            entity.HasIndex(e => e.LoteProductoId, "IX_DetallePedido_LoteProductoId");
            entity.HasIndex(e => e.PedidoId, "IX_DetallePedido_PedidoId");
            entity.HasIndex(e => e.ProductoId, "IX_DetallePedido_ProductoId");
            entity.Property(e => e.PrecioUnitario).HasPrecision(10, 2);
            entity.Property(e => e.Subtotal).HasPrecision(10, 2);

            entity.HasOne(d => d.LoteProducto)
                .WithMany(p => p.Detallepedidos)
                .HasForeignKey(d => d.LoteProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_LoteProducto");

            entity.HasOne(d => d.Pedido)
                .WithMany(p => p.Detallepedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_Pedido");

            entity.HasOne(d => d.Producto)
                .WithMany(p => p.Detallepedidos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_Producto");
        }
    }
}
