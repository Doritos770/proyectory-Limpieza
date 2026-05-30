using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data.Configurations
{
    public class CodigoSeguridadConfiguration : IEntityTypeConfiguration<Codigoseguridad>
    {
        public void Configure(EntityTypeBuilder<Codigoseguridad> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("codigoseguridad");
            entity.HasIndex(e => e.Codigo, "IX_CodigoSeguridad_Codigo");
            entity.HasIndex(e => e.UsuarioId, "IX_CodigoSeguridad_UsuarioId");
            entity.Property(e => e.Codigo).HasMaxLength(6);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.Usado)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Codigoseguridads)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_CodigoSeguridad_Usuario");
        }
    }
}
