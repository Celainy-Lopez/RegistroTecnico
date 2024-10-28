using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace RegistroTecnico.Services;

public class TrabajoService(IDbContextFactory<Context> DbFactory)
{

	private async Task<bool> Existe(int id)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos.AnyAsync(c => c.TrabajoId == id);
	}

	private async Task<bool> Insertar(Trabajos trabajo)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Trabajos.Add(trabajo);
		await AfectarArticulo(trabajo.TrabajosDetalle.ToArray());
		return await contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Trabajos trabajo)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var trabajoOriginal = await contexto.Trabajos
		.Include(t => t.TrabajosDetalle)
		.AsNoTracking()
		.FirstOrDefaultAsync(t => t.TrabajoId == trabajo.TrabajoId);

		await AfectarArticulo(trabajoOriginal.TrabajosDetalle.ToArray(), false);

		await AfectarArticulo(trabajo.TrabajosDetalle.ToArray(), true);

		contexto.Update(trabajo);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int trabajoId)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var trabajo = contexto.Trabajos.Find(trabajoId);

		await AfectarArticulo(trabajo.TrabajosDetalle.ToArray(), false);

		contexto.TrabajosDetalles.RemoveRange(trabajo.TrabajosDetalle);
		contexto.Trabajos.Remove(trabajo);

		var cantidad = await contexto.SaveChangesAsync();
		return cantidad > 0;
	}

	public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos.AsNoTracking()
			.Where(criterio)
			.Include(t=>t.Tecnico)
				.ThenInclude(t=>t.TipoTecnico)
			.Include(t=>t.Cliente)
			.Include(t => t.Prioridad)
			.Include(t => t.TrabajosDetalle)
			.ToListAsync();
	}

	public async Task<Trabajos?> Buscar(int id)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos
			.Include(t => t.Tecnico)
				.ThenInclude(t => t.TipoTecnico)
			.Include(t => t.Cliente)
			.Include(t => t.Prioridad)
			.Include(t => t.TrabajosDetalle)
			.FirstOrDefaultAsync(c => c.TrabajoId == id);
	}

	public async Task<bool> ValidarTrabajo(string descripcionTrabajo)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos.AnyAsync(c => c.DescripcionTrabajo == descripcionTrabajo);
	}

	public async Task<bool> Guardar(Trabajos trabajo)
	{
		if (!await Existe(trabajo.TrabajoId))
			return await Insertar(trabajo);
		else
			return await Modificar(trabajo);
	}

	private async Task AfectarArticulo(TrabajosDetalle[] detalle, bool resta = true)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalle)
		{
			var articulo = await contexto.Articulos.SingleAsync(p => p.ArticuloId == item.ArticuloId);
			if (resta)
			{
				articulo.Existencia -= item.Cantidad;
			}
			else
			{
				articulo.Existencia += item.Cantidad;
			}
            await contexto.SaveChangesAsync();
        }
    }
}