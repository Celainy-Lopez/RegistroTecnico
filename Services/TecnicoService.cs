using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

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
            return await _context.Tecnicos.AnyAsync(c => c.tecnicoId == id);
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
            var tecnicos = await _context.Tecnicos.FirstOrDefaultAsync(c => c.tecnicoId == id);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Tecnicos> Buscar(int id)
        {
            return await _context.Tecnicos.AsNoTracking().
                FirstOrDefaultAsync(c => c.tecnicoId == id);
        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            return _context.Tecnicos.AsNoTracking()
                .Where(criterio)
                .ToList();
        }
    }
}
