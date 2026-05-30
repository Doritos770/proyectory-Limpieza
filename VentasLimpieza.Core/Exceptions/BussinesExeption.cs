using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VentasLimpieza.Core.Exceptions
{
    public class BussinesExeption : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public Object? Details { get; }
        public BussinesExeption(string message, HttpStatusCode
            statusCode = HttpStatusCode.BadRequest,object? details = null)
            : base(message)
        {
            StatusCode = statusCode;
            Details = details;
        }
    }
}
