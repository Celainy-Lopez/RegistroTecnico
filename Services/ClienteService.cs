using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;


namespace RegistroTecnico.Services;

public class ClienteService(Context context)
{
	private readonly Context _context = context;

	private async Task<bool> Existe(int id)
	{
		return await _context.Clientes.AnyAsync(tT => tT.ClienteId == id);
	}

	private async Task<bool> Insertar(Clientes cliente)
	{
		_context.Clientes.Add(cliente);
		return await _context.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Clientes cliente)
	{
		_context.Update(cliente);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		var cliente = await _context.Clientes.FirstOrDefaultAsync(tT => tT.ClienteId == id);
		if (cliente != null)
		{
			_context.Clientes.Remove(cliente);
			return await _context.SaveChangesAsync() > 0;
		}
		return false;
	}

	public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
	{
		return _context.Clientes.AsNoTracking()
			.Where(criterio)
			.ToList();
	}


	public async Task<Clientes?> Buscar(int id)
	{
		return await _context.Clientes
			.AsNoTracking()
			.FirstOrDefaultAsync(tT => tT.ClienteId == id);
	}

	public async Task<bool> ValidarCliente(string nombres)
	{
		return await _context.Clientes.AnyAsync(tT => tT.Nombres == nombres);
	}

	public async Task<bool> ValidarWhatsApp(string numero)
	{
		return await _context.Clientes.AnyAsync(tT => tT.WhatsApp == numero);
	}
	public async Task<bool> Guardar(Clientes cliente)
	{
		if (!await Existe(cliente.ClienteId))
			return await Insertar(cliente);
		else
			return await Modificar(cliente);
	}
}

