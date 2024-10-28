using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class CotizacionesService(IDbContextFactory<Context> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
        return await contexto.Cotizaciones
            .AnyAsync(a => a.CotizacionId == id);
    }

    private async Task<bool> Insertar(Cotizaciones cotizaciones)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
        contexto.Cotizaciones
            .Add(cotizaciones);
        return await contexto
            .SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Cotizaciones cotizaciones)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var cotizacionOriginal = await contexto.Cotizaciones
            .Include(t => t.CotizacionesDetalle)
            .FirstOrDefaultAsync(t => t.CotizacionId == cotizaciones.CotizacionId);

        if (cotizacionOriginal == null)
            return false;

        foreach (var detalleOriginal in cotizacionOriginal.CotizacionesDetalle)
        {
            if (!cotizaciones.CotizacionesDetalle.Any(d => d.DetalleId == detalleOriginal.DetalleId))
            {
                contexto.CotizacionesDetalle.Remove(detalleOriginal);
            }
        }

        contexto.Entry(cotizacionOriginal).CurrentValues.SetValues(cotizaciones);

        foreach (var detalle in cotizaciones.CotizacionesDetalle)
        {
            var detalleExistente = cotizacionOriginal.CotizacionesDetalle
                .FirstOrDefault(d => d.DetalleId == detalle.DetalleId);

            if (detalleExistente != null)
            {
                contexto.Entry(detalleExistente).CurrentValues.SetValues(detalle);
            }
            else
            {
                cotizacionOriginal.CotizacionesDetalle.Add(detalle);
            }
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cotizacion = await contexto.Cotizaciones
            .Include(t => t.CotizacionesDetalle)
            .ThenInclude(td => td.Articulo)
            .FirstOrDefaultAsync(t => t.CotizacionId == id);

        if (cotizacion == null)
            return false;

        contexto.CotizacionesDetalle.RemoveRange(cotizacion.CotizacionesDetalle);
        contexto.Cotizaciones.Remove(cotizacion);

        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(t => t.Cliente)
            .Include(t => t.CotizacionesDetalle)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();

    }

    public async Task<Cotizaciones> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(t => t.Cliente)
            .Include(t => t.CotizacionesDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.CotizacionId == id);
    }

    public async Task<Cotizaciones> BuscarDetalles(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(t => t.Cliente)
            .Include(t => t.CotizacionesDetalle)
            .ThenInclude(td => td.Articulo)
            .FirstOrDefaultAsync(p => p.CotizacionId == id);
    }

    public async Task<bool> Guardar(Cotizaciones cotizaciones)
    {
        if (!await Existe(cotizaciones.CotizacionId))
            return await Insertar(cotizaciones);
        else
            return await Modificar(cotizaciones);
    }
}