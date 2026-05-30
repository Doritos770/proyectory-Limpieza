using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace VentasLimpieza.Api.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Comprueba que la API está funcionando y retorna la hora actual del servidor.
        /// </summary>
        /// <remarks>Este método devuelve un objeto anónimo indicando que la conexión fue exitosa y la fecha actual. 
        /// Sirve como un ping simple para monitoreo.</remarks>
        /// <returns>Un <see cref="IActionResult"/> que contiene un objeto con un mensaje de éxito y la fecha actual.</returns>
        /// <response code="200">Retorna el objeto anónimo con mensaje de conexión exitosa.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                mensaje = "Conexión exitosa",
                fecha = DateTime.Now
            });
        }

        /// <summary>
        /// Endpoint alternativo para comprobar el estado de conexión de la API.
        /// </summary>
        /// <remarks>Devuelve un objeto anónimo con un mensaje indicando conexión exitosa, pero sin la fecha del servidor.</remarks>
        /// <returns>Un <see cref="IActionResult"/> que contiene un objeto con un mensaje de éxito.</returns>
        /// <response code="200">Retorna el mensaje indicando que la conexión fue exitosa.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("con")]
        public IActionResult Conexion()
        {
            return Ok(new { mensaje = "Conexión exitosa" });
        }
    }
}
