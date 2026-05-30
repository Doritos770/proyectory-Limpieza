using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;

namespace VentasLimpieza.Services.Validators
{
    public class ProductoPorLoteValidator : AbstractValidator<ProductoPorLote>
    {
        public ProductoPorLoteValidator()
        {
            RuleFor(x => x.Categoria)
                .NotEmpty().WithMessage("La categoría es obligatoria");

            RuleFor(x => x.CantidadProductos)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad de productos no puede ser negativa");

            RuleFor(x => x.CantidadLotes)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad de lotes no puede ser negativa");

            RuleFor(x => x.TotalUnidades)
                .GreaterThanOrEqualTo(0).WithMessage("El total de unidades no puede ser negativo");

            RuleFor(x => x.ValorInventario)
                .GreaterThanOrEqualTo(0).WithMessage("El valor del inventario no puede ser negativo");
        }
    }
}
