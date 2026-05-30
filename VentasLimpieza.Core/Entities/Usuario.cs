namespace VentasLimpieza.Core.Entities
{

    /// <summary>
    /// Entidad de dominio central que modela el perfil de un usuario o cliente de la plataforma de ventas.
    /// </summary>
    public partial class Usuario : BaseEntity
    {
     //   public int Id { get; set; }

        /// <summary>
        /// Dirección de correo electrónico y medio de contacto principal.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Contraseña almacenada.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Nombres del cliente.
        /// </summary>
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Apellidos del cliente.
        /// </summary>
        public string Apellido { get; set; } = null!;

        /// <summary>
        /// Teléfono celular o fijo para contacto.
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Fecha original de alta en el sistema.
        /// </summary>
        public DateOnly FechaRegistro { get; set; }

        /// <summary>
        /// Estado actual de la cuenta (activa, baneada, eliminada lógicamente).
        /// </summary>
        public ulong IsActive { get; set; }

        /// <summary>
        /// Códigos de seguridad generados para este usuario.
        /// </summary>
        public virtual ICollection<Codigoseguridad> Codigoseguridads { get; set; } = new List<Codigoseguridad>();

        /// <summary>
        /// Direcciones asociadas a este usuario.
        /// </summary>
        public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

        /// <summary>
        /// Historial de pedidos realizados.
        /// </summary>
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        /// <summary>
        /// Reseñas que ha publicado.
        /// </summary>
        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }

}