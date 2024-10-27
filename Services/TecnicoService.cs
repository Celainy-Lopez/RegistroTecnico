using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnico.Services;

public class TecnicoService(IDbContextFactory<Context> DbFactory)
{

	private async Task<bool> Existe(int id)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Tecnicos.AnyAsync(c => c.TecnicoId == id);
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Tecnicos.Add(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task <bool> Modificar(Tecnicos tecnico)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Update(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var tecnico = await contexto.Tecnicos.FirstOrDefaultAsync(c => c.TecnicoId == id);
        if(tecnico != null)
        {
            contexto.Tecnicos.Remove(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Tecnicos.AsNoTracking()
			.Include(t => t.TipoTecnico)
			.Where(criterio)
			.ToListAsync();
    }

    public async Task<Tecnicos?> Buscar (int id)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Tecnicos
            .Include(t=>t.TipoTecnico)
            .FirstOrDefaultAsync(c => c.TecnicoId == id);
    }

    public async Task<bool> ValidarTecnico(string nombre)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Tecnicos.AnyAsync(c => c.NombreTecnico == nombre);
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

}
