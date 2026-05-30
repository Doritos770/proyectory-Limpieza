using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data
{
    public class ProductorConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("producto");
            entity.HasIndex(e => e.CategoriaId, "IX_Producto_CategoriaId");
            entity.HasIndex(e => e.Marca, "IX_Producto_Marca");
            entity.HasIndex(e => e.Nombre, "IX_Producto_Nombre");
            entity.HasIndex(e => e.Precio, "IX_Producto_Precio");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.ImagenUrl).HasMaxLength(500);
            entity.Property(e => e.Marca).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Precio).HasPrecision(10, 2);
            entity.Property(e => e.Presentacion).HasMaxLength(100);

            entity.HasOne(d => d.Categoria)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria");
        }
    }
}