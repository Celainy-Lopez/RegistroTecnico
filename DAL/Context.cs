using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using SQLitePCL;

namespace RegistroTecnico.DAL;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
	public DbSet<Tecnicos> Tecnicos { get; set; }

	public DbSet<TiposTecnicos> TiposTecnicos { get; set; }

	public DbSet<Clientes> Clientes {  get; set; }

	public DbSet<Trabajos> Trabajos { get; set; }

}
