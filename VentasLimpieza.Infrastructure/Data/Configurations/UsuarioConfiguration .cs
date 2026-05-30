using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data.Configurations;

public class UsuariosConfiguration :
    IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");
        entity.ToTable("usuario");
        entity.HasIndex(e => e.FechaRegistro, "IX_Usuario_FechaRegistro");
        entity.HasIndex(e => e.Email, "UQ_Email").IsUnique();
        entity.Property(e => e.Apellido).HasMaxLength(50);
        entity.Property(e => e.Email).HasMaxLength(100);
        entity.Property(e => e.IsActive)
            .HasDefaultValueSql("b'1'")
            .HasColumnType("bit(1)");
        entity.Property(e => e.Nombre).HasMaxLength(50);
        entity.Property(e => e.Password).HasMaxLength(255);
        entity.Property(e => e.Telefono).HasMaxLength(20);
    }
}
