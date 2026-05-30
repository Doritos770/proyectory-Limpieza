using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data
{
    public class CattegorialConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("categoria");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.ImagenUrl).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasColumnType("enum('desinfectantes','detergentes','limpiadores','lubricantes','ambientadores')");
        }
    }
}