using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnico.Services
{
    public class TecnicoService
    {
        private readonly Context _context;

        public TecnicoService(Context context)
        {
            _context = context; 
        }

        public async Task<bool> Existe(int id)
        {
            return await _context.Tecnicos.AnyAsync(c => c.TecnicoId == id);
        }

        public async Task<bool> Insertar(Tecnicos tecnicos)
        {
            _context.Tecnicos.Add(tecnicos);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task <bool> Modificar(Tecnicos tecnicos)
        {
            _context.Update(tecnicos);
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
            return _context.Tecnicos.AsNoTracking()
                .Where(criterio)
                .ToList();
        }

        public async Task<Tecnicos?> BuscarNombres(string nombre)
        {
            return await _context.Tecnicos.AsNoTracking()
                .FirstOrDefaultAsync(c=>c.NombreTecnico == nombre);
        }

        public async Task<Tecnicos?> Buscar (int id)
        {
            return await _context.Tecnicos
                .AsNoTracking()
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
}
