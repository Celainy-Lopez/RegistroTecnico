using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class ArticuloService(Context context)
{
	private readonly Context _context = context;

	public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
	{
		return await _context.Articulos
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}
