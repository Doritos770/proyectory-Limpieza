using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Enum;

namespace VentasLimpieza.Core.Entities
{
    public partial class Security : BaseEntity
    {
        //public int Id { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public RoleType Role { get; set; }
    }
}
