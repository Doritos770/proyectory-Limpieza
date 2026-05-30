using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VentasLimpieza.Core.Enum;

namespace VentasLimpieza.Core.Interfaces
{
    public interface IDbConnectionFactory
    {
        DataBaseProvider Provider { get; }
        IDbConnection CreateConnection();
    }
}
