using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class SecurityRepository :
        BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(VentasLimpiezaContext context)
            : base(context)
        {

        }

        public async Task<Security>
            GetLoginByCredentials(UserLogin userLogin)
        {
            return await _entities.FirstOrDefaultAsync
                (x => x.Login == userLogin.User);
            //&& x.Password == userLogin.Password);
        }
    }
}
