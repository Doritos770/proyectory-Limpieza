using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasLimpieza.Core.CustomEntities
{
    /// <summary>
    /// Modelo independiente que transporta exclusivamente los metadatos de paginación 
    /// (se suele adjuntar en el header HTTP o como propiedad del ApiResponse).
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Total de elementos en la fuente de datos.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Cantidad máxima de elementos por página.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Índice actual de la página.
        /// </summary>
        public int CurrentePage { get; set; }

        /// <summary>
        /// Total de páginas disponibles.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Indica si hay más resultados en páginas posteriores.
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Indica si hay resultados en páginas anteriores.
        /// </summary>
        public bool HasPreviousPage { get; set; }

        public Pagination()
        {

        }

        public Pagination(PagedList<object> lista)
        {
            TotalCount = lista.Count;
            PageSize = lista.PageSize;
            CurrentePage = lista.CurrentPage;
            TotalPages = lista.TotalPages;
            HasNextPage = lista.HasNextPage;
            HasPreviousPage = lista.HasPreviousPage;
        }
    }
}
