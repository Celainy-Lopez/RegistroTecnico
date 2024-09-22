using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;


namespace RegistroTecnico.Services;

public class PrioridadService(Context context)
{
	private readonly Context _context = context;

	private async Task<bool> Existe(int id)
	{
		return await _context.Prioridades.AnyAsync(tT => tT.PrioridadId == id);
	}

	private async Task<bool> Insertar(Prioridades prioridad)
	{
		_context.Prioridades.Add(prioridad);
		return await _context.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Prioridades prioridad)
	{
		_context.Update(prioridad);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		var prioridad = await _context.Prioridades.FirstOrDefaultAsync(tT => tT.PrioridadId == id);
		if (prioridad != null)
		{
			_context.Prioridades.Remove(prioridad);
			return await _context.SaveChangesAsync() > 0;
		}
		return false;
	}

	public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
	{
		return _context.Prioridades.AsNoTracking()
			.Where(criterio)
			.ToList();
	}

	public async Task<Prioridades?> Buscar(int id)
	{
		return await _context.Prioridades
			.AsNoTracking()
			.FirstOrDefaultAsync(tT => tT.PrioridadId == id);
	}

	public async Task<bool> ValidarPrioridad(string descripcion)
	{
		return await _context.Prioridades.AnyAsync(tT => tT.DescripcionPrioridad == descripcion);
	}

	public async Task<bool> Guardar(Prioridades prioridad)
	{
		if (!await Existe(prioridad.PrioridadId))
			return await Insertar(prioridad);
		else
			return await Modificar(prioridad);
	}
}

