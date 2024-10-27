using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;


namespace RegistroTecnico.Services;

public class ClienteService(IDbContextFactory<Context> DbFactory)
{

	private async Task<bool> Existe(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes.AnyAsync(tT => tT.ClienteId == id);
	}

	private async Task<bool> Insertar(Clientes cliente)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Clientes.Add(cliente);
		return await contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Clientes cliente)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Update(cliente);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var cliente = await contexto.Clientes.FirstOrDefaultAsync(tT => tT.ClienteId == id);
		if (cliente != null)
		{
			contexto.Clientes.Remove(cliente);
			return await contexto.SaveChangesAsync() > 0;
		}
		return false;
	}

	public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return contexto.Clientes.AsNoTracking()
			.Where(criterio)
			.ToList();
	}


	public async Task<Clientes?> Buscar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes
			.AsNoTracking()
			.FirstOrDefaultAsync(tT => tT.ClienteId == id);
	}

	public async Task<bool> ValidarCliente(string nombres)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes.AnyAsync(tT => tT.Nombres == nombres);
	}

	public async Task<bool> ValidarWhatsApp(string numero)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes.AnyAsync(tT => tT.WhatsApp == numero);
	}
	public async Task<bool> Guardar(Clientes cliente)
	{
		if (!await Existe(cliente.ClienteId))
			return await Insertar(cliente);
		else
			return await Modificar(cliente);
	}
}

