using FluentValidation;
using VentasLimpieza.Core.Dtos;
using VentasLimpieza.Core.Helpers;

namespace VentasLimpieza.Services.Validators
{
    public class LoteproductoDtoValidator : AbstractValidator<LoteproductoDto>
    {
        public LoteproductoDtoValidator()
        {
            RuleFor(x => x.ProductoId)
                .NotEmpty().WithMessage("El ID del producto es obligatorio")
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor a 0");

            RuleFor(x => x.NumeroLote)
                .NotEmpty().WithMessage("El número de lote es obligatorio")
                .MaximumLength(50).WithMessage("El número de lote no puede exceder los 50 caracteres");

            RuleFor(x => x.FechaFabricacion)
                .NotEmpty().WithMessage("La fecha de fabricación es obligatoria")
                .Must(fecha => Procesos.ParseFechaFlexible(fecha) != null)
                .WithMessage("La fecha de fabricación no tiene un formato válido");

            RuleFor(x => x.FechaCaducidad)
                .NotEmpty().WithMessage("La fecha de caducidad es obligatoria")
                .Must(fecha => Procesos.ParseFechaFlexible(fecha) != null)
                .WithMessage("La fecha de caducidad no tiene un formato válido")
                .Must((dto, fechaCaducidad) =>
                {
                    string fechaFab = Procesos.ParseFechaFlexible(dto.FechaFabricacion);
                    string fechaCad = Procesos.ParseFechaFlexible(fechaCaducidad);

                    if (fechaFab == null || fechaCad == null)
                        return true; // Ya se validó antes, dejamos pasar

                    if (DateTime.TryParse(fechaFab, out DateTime dtFab) &&
                        DateTime.TryParse(fechaCad, out DateTime dtCad))
                    {
                        return dtCad > dtFab;
                    }
                    return false;
                })
                .WithMessage("La fecha de caducidad debe ser posterior a la fecha de fabricación");

            RuleFor(x => x.CantidadIngreso)
                .GreaterThan(0).WithMessage("La cantidad de ingreso debe ser mayor a 0");

            RuleFor(x => x.CantidadDisponible)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad disponible no puede ser negativa")
                .LessThanOrEqualTo(x => x.CantidadIngreso)
                .WithMessage("La cantidad disponible no puede ser mayor a la cantidad de ingreso");

            RuleFor(x => x.PrecioCompra)
                .GreaterThan(0).WithMessage("El precio de compra debe ser mayor a 0");

            RuleFor(x => x.UbicacionAlmacen)
                .MaximumLength(100).WithMessage("La ubicación no puede exceder los 100 caracteres");
        }
    }
}