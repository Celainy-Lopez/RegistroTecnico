using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;


namespace RegistroTecnico.Services;

public class PrioridadService(IDbContextFactory<Context> DbFactory)
{

	private async Task<bool> Existe(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Prioridades.AnyAsync(tT => tT.PrioridadId == id);
	}

	private async Task<bool> Insertar(Prioridades prioridad)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Prioridades.Add(prioridad);
		return await contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Prioridades prioridad)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Update(prioridad);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var prioridad = await contexto.Prioridades.FirstOrDefaultAsync(tT => tT.PrioridadId == id);
		if (prioridad != null)
		{
			contexto.Prioridades.Remove(prioridad);
			return await contexto.SaveChangesAsync() > 0;
		}
		return false;
	}

	public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return contexto.Prioridades.AsNoTracking()
			.Where(criterio)
			.ToList();
	}

	public async Task<Prioridades?> Buscar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Prioridades
			.AsNoTracking()
			.FirstOrDefaultAsync(tT => tT.PrioridadId == id);
	}

	public async Task<bool> ValidarPrioridad(string descripcion)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Prioridades.AnyAsync(tT => tT.DescripcionPrioridad == descripcion);
	}

	public async Task<bool> Guardar(Prioridades prioridad)
	{
		if (!await Existe(prioridad.PrioridadId))
			return await Insertar(prioridad);
		else
			return await Modificar(prioridad);
	}
}

