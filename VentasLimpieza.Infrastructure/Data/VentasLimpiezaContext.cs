using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Data;

public partial class VentasLimpiezaContext : DbContext
{
    public VentasLimpiezaContext()
    {
    }

    public VentasLimpiezaContext(DbContextOptions<VentasLimpiezaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Codigoseguridad> Codigoseguridads { get; set; }

    public virtual DbSet<Detallepedido> Detallepedidos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Loteproducto> Loteproductos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Resena> Resenas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Security> Securities { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseMySql("server=localhost;port=3306;database=DbVentasLimpieza;uid=root;pwd=dor13", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.44-mysql"));


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
