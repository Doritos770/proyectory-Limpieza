using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Services.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}
