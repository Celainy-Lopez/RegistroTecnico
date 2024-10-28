using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class ArticuloService(IDbContextFactory<Context> DbFactory)
{

    public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Articulos
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}
