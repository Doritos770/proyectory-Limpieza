using System.Net;
using System.Text.Json.Serialization;
using VentasLimpieza.Core.CustomEntities;

namespace VentasLimpieza.Core.CustomEntities
{
    /// <summary>
    /// Envoltorio interno usado por los servicios para devolver información de paginación,
    /// estado y notificaciones a la capa del controlador.
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// Objeto de lista paginada genérica.
        /// </summary>
        public PagedList<object> Pagination { get; set; }

        /// <summary>
        /// Arreglo de mensajes de negocio o validación generados por el servicio.
        /// </summary>
        public Message[] Messages { get; set; }

        /// <summary>
        /// Código de estado HTTP que refleja el resultado de la operación en la lógica de negocio.
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
    }
}
