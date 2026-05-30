using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories;

public class DireccionRepository //: IDireccionRepository
{
    public readonly VentasLimpiezaContext _direccion;

    public DireccionRepository(VentasLimpiezaContext direccion)
    {
        _direccion =direccion ;
    }

    public async Task<IEnumerable<Direccion>> GetDireccionesAsync()
    {
        var direcciones = await _direccion.Direccions.ToListAsync();
        return direcciones;
    }

    public async Task<Direccion> GetDireccionByIdAsync(int id)
    {
        var direccione = await _direccion.Direccions.FirstOrDefaultAsync(x => x.Id == id);
        return direccione;
    }
    public async Task InsertDireccion(Direccion direccion)
    {
        _direccion.Direccions.Add(direccion);
        await _direccion.SaveChangesAsync();
    }

    public async Task UpdateDireccion(Direccion direccion)
    {
        _direccion.Direccions.Update(direccion);
        await _direccion.SaveChangesAsync();
    }  
    public async Task DeleteDireccion(Direccion direccion)
    {
        _direccion.Direccions.Remove(direccion);
        await _direccion.SaveChangesAsync();
    }
}
