using System.Net;
using System.Text.Json.Serialization;
using VentasLimpieza.Core.CustomEntities;

namespace VentasLimpieza.Core.CustomEntities
{
    public class ResponseData
    {
        public PagedList<object> Pagination { get; set; }
        public Message[] Messages { get; set; }
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
    }
}
