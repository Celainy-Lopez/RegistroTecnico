using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TrabajosDetalleService(Context context)
{
	private readonly Context _context = context;

	public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
	{
		return await _context.Trabajos
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}
