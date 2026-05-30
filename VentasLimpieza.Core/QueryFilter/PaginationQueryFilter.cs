using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasLimpieza.Core.QueryFilters
{
    public class PaginationQueryFilter
    {
        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; }
    }
}
