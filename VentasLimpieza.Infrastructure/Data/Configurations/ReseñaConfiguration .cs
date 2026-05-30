using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data
{
    public class ReseñaConfiguration : IEntityTypeConfiguration<Resena>
    {
        public void Configure(EntityTypeBuilder<Resena> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("resena");
            entity.HasIndex(e => e.Calificacion, "IX_Resena_Calificacion");
            entity.HasIndex(e => e.ProductoId, "IX_Resena_ProductoId");
            entity.HasIndex(e => e.UsuarioId, "IX_Resena_UsuarioId");
            entity.Property(e => e.Comentario).HasColumnType("text");

            entity.HasOne(d => d.Producto)
                .WithMany(p => p.Resenas)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resena_Producto");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Resenas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resena_Usuario");
        }
    }
}
