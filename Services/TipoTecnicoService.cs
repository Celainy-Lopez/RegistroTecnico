using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;


namespace RegistroTecnico.Services;

public class TipoTecnicoService(Context context)
{
	private readonly Context _context = context;

    private async Task<bool> Existe(int id)
	{
		return await _context.TiposTecnicos.AnyAsync(tT=>tT.TipoTecnicoId == id);
	
	}

	private async Task<bool> Insertar(TiposTecnicos tiposTecnicos)
	{
		_context.TiposTecnicos.Add(tiposTecnicos);
		return await _context.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(TiposTecnicos tiposTecnicos)
	{
		_context.Update(tiposTecnicos);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool>Guardar(TiposTecnicos tiposTecnicos)
	{
		if(!await Existe(tiposTecnicos.TipoTecnicoId))
		{
			return await Insertar(tiposTecnicos);
		}
		else
		{
			return await Modificar(tiposTecnicos);
		}
	}

	public async Task<bool>Eliminar(int id)
	{
		var tiposTecnico = await _context.TiposTecnicos.
			Where(tT=>tT.TipoTecnicoId == id).ExecuteDeleteAsync();
		return tiposTecnico > 0;
	}

	public async Task<TiposTecnicos> Buscar(int id)
	{
		return await _context.TiposTecnicos.
			AsNoTracking()
			.FirstOrDefaultAsync(tT=> tT.TipoTecnicoId == id);
	}

	public List<TiposTecnicos>Listar(Expression<Func<TiposTecnicos, bool>> criterio)
	{
		return _context.TiposTecnicos.
			AsNoTracking()
			.Where(criterio)
			.ToList();
	}
}
