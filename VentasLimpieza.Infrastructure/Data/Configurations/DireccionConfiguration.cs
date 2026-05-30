using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data.Configurations
{
    public class DireccionConfiguration : IEntityTypeConfiguration<Direccion>
    {
        public void Configure(EntityTypeBuilder<Direccion> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("direccion");
            entity.HasIndex(e => e.Principal, "IX_Direccion_Principal");
            entity.HasIndex(e => e.UsuarioId, "IX_Direccion_UsuarioId");
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Direccion1)
                .HasMaxLength(200)
                .HasColumnName("Direccion");
            entity.Property(e => e.Pais).HasMaxLength(50);
            entity.Property(e => e.Principal)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.Provincia).HasMaxLength(100);

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Direccions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Direccion_Usuario");
        }
    }
}
