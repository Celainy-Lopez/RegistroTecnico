using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;


namespace RegistroTecnico.Services;

public class TipoTecnicoService(IDbContextFactory<Context> DbFactory)
{
	private async Task<bool> Existe(int id)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.TiposTecnicos.AnyAsync(tT => tT.TipoTecnicoId == id);
    }

    private async Task<bool> Insertar(TiposTecnicos tipoTecnico)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.TiposTecnicos.Add(tipoTecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task <bool> Modificar(TiposTecnicos tipoTecnico)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Update(tipoTecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var tipoTecnico = await contexto.TiposTecnicos.FirstOrDefaultAsync(tT => tT.TipoTecnicoId == id);
        if(tipoTecnico != null)
        {
            contexto.TiposTecnicos.Remove(tipoTecnico);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.TiposTecnicos
		    .AsNoTracking()
		    .Where(criterio)
		    .ToListAsync();
	}

    public async Task<TiposTecnicos?> Buscar (int id)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(tT => tT.TipoTecnicoId == id);
    }

    public async Task<bool> ValidarTipoTecnico(string descripcion)
    {
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.TiposTecnicos.AnyAsync(tT => tT.Descripcion == descripcion);
    }

    public async Task<bool> Guardar(TiposTecnicos tipoTecnico)
    {
        if (!await Existe(tipoTecnico.TipoTecnicoId))
            return await Insertar(tipoTecnico);
        else
            return await Modificar(tipoTecnico);
    }
}
