namespace VentasLimpieza.Core.CustomEntities
{
    /// <summary>
    /// Extensión de lista que incluye capacidades de paginación de manera genérica.
    /// </summary>
    /// <typeparam name="T">El tipo de objeto contenido en la lista.</typeparam>
    public class PagedList<T> : List<T>
    {
        #region Atributos
        /// <summary>
        /// Página actual en la que se encuentra la lista.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Total de páginas calculadas según el total de registros y el tamaño de página.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Límite de registros mostrados por página.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Cantidad total de registros encontrados en la consulta sin paginar.
        /// </summary>
        public int TotalCount { get; set; }
        #endregion

        #region Propiedades
        /// <summary>
        /// Indica si existe una página anterior.
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Indica si existe una página posterior.
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// El número de la página siguiente, si existe.
        /// </summary>
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : null;

        /// <summary>
        /// El número de la página anterior, si existe.
        /// </summary>
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : null;
        #endregion

        public PagedList(List<T> items,
            int count,
            int pageNumber,
            int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> Create
            (IEnumerable<T> source,
            int pageNumber,
            int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedList<T>
                (items, count, pageNumber, pageSize);
        }
    }
}
