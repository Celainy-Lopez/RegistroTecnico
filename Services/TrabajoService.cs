using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
		return await _context.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Trabajos trabajo)
	{
		_context.Update(trabajo);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		var trabajo = await _context.Trabajos.FirstOrDefaultAsync(c => c.TrabajoId == id);
		if (trabajo != null)
		{
			_context.Trabajos.Remove(trabajo);
			return await _context.SaveChangesAsync() > 0;
		}
		return false;
	}

	public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
	{
		return _context.Trabajos.AsNoTracking()
			.Where(criterio)
			.Include(t=>t.Tecnico)
				.ThenInclude(t=>t.TipoTecnico)
			.Include(t=>t.Cliente)
			.ToList();
	}

	public async Task<Trabajos?> Buscar(int id)
	{
		return await _context.Trabajos
			.Include(t => t.Tecnico)
				.ThenInclude(t => t.TipoTecnico)
			.Include(t => t.Cliente)
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

}