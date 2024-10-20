using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace RegistroTecnico.Services;

public class TrabajoService(Context context)
{
	private readonly Context _context = context;

	private async Task<bool> Existe(int id)
	{
		return await _context.Trabajos.AnyAsync(c => c.TrabajoId == id);
	}

	private async Task<bool> Insertar(Trabajos trabajo)
	{
		_context.Trabajos.Add(trabajo);
		await AfectarArticulo(trabajo.TrabajosDetalle.ToArray());
		return await _context.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Trabajos trabajo)
	{
		var trabajoOriginal = await _context.Trabajos
		.Include(t => t.TrabajosDetalle)
		.AsNoTracking()
		.FirstOrDefaultAsync(t => t.TrabajoId == trabajo.TrabajoId);

		await AfectarArticulo(trabajoOriginal.TrabajosDetalle.ToArray(), false);

		await AfectarArticulo(trabajo.TrabajosDetalle.ToArray(), true);

		_context.Update(trabajo);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int trabajoId)
	{
		var trabajo = _context.Trabajos.Find(trabajoId);

		await AfectarArticulo(trabajo.TrabajosDetalle.ToArray(), false);

		_context.TrabajosDetalles.RemoveRange(trabajo.TrabajosDetalle);
		_context.Trabajos.Remove(trabajo);

		var cantidad = await _context.SaveChangesAsync();
		return cantidad > 0;
	}

	public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
	{
		return await _context.Trabajos.AsNoTracking()
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
		return await _context.Trabajos
			.Include(t => t.Tecnico)
				.ThenInclude(t => t.TipoTecnico)
			.Include(t => t.Cliente)
			.Include(t => t.Prioridad)
			.Include(t => t.TrabajosDetalle)
			.FirstOrDefaultAsync(c => c.TrabajoId == id);
	}

	public async Task<bool> ValidarTrabajo(string descripcionTrabajo)
	{
		return await _context.Trabajos.AnyAsync(c => c.DescripcionTrabajo == descripcionTrabajo);
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
		foreach (var item in detalle)
		{
			var articulo = await _context.Articulos.SingleAsync(p => p.ArticuloId == item.ArticuloId);
			if (resta)
			{
				articulo.Existencia -= item.Cantidad;
			}
			else
			{
				articulo.Existencia += item.Cantidad;
			}
		}
	}
}