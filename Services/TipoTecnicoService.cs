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
        return await _context.TiposTecnicos.AnyAsync(tT => tT.TipoTecnicoId == id);
    }

    private async Task<bool> Insertar(TiposTecnicos tipoTecnico)
    {
        _context.TiposTecnicos.Add(tipoTecnico);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task <bool> Modificar(TiposTecnicos tipoTecnico)
    {
        _context.Update(tipoTecnico);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var tipoTecnico = await _context.TiposTecnicos.FirstOrDefaultAsync(tT => tT.TipoTecnicoId == id);
        if(tipoTecnico != null)
        {
            _context.TiposTecnicos.Remove(tipoTecnico);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return _context.TiposTecnicos.AsNoTracking()
            .Where(criterio)
            .ToList();
    }

    public async Task<TiposTecnicos?> BuscarDescripcion(string descripcion)
    {
        return await _context.TiposTecnicos.AsNoTracking()
            .FirstOrDefaultAsync(tT=>tT.Descripcion == descripcion);
    }

    public async Task<TiposTecnicos?> Buscar (int id)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(tT => tT.TipoTecnicoId == id);
    }

    public async Task<bool> ValidarTipoTecnico(string descripcion)
    {
        return await _context.TiposTecnicos.AnyAsync(tT => tT.Descripcion == descripcion);
    }

    public async Task<bool> Guardar(TiposTecnicos tipoTecnico)
    {
        if (!await Existe(tipoTecnico.TipoTecnicoId))
            return await Insertar(tipoTecnico);
        else
            return await Modificar(tipoTecnico);
    }
}
