﻿using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnico.Services;

public class TecnicoService(Context context)
{
    private readonly Context _context = context;

	private async Task<bool> Existe(int id)
    {
        return await _context.Tecnicos.AnyAsync(c => c.TecnicoId == id);
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        _context.Tecnicos.Add(tecnico);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task <bool> Modificar(Tecnicos tecnico)
    {
        _context.Update(tecnico);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var tecnico = await _context.Tecnicos.FirstOrDefaultAsync(c => c.TecnicoId == id);
        if(tecnico != null)
        {
            _context.Tecnicos.Remove(tecnico);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return await _context.Tecnicos.AsNoTracking()
			.Include(t => t.TipoTecnico)
			.Where(criterio)
			.ToListAsync();
    }

    public async Task<Tecnicos?> Buscar (int id)
    {
        return await _context.Tecnicos
            .Include(t=>t.TipoTecnico)
            .FirstOrDefaultAsync(c => c.TecnicoId == id);
    }

    public async Task<bool> ValidarTecnico(string nombre)
    {
        return await _context.Tecnicos.AnyAsync(c => c.NombreTecnico == nombre);
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

}
