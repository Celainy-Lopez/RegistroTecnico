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

	public DbSet<Prioridades> Prioridades { get; set; }

	public DbSet<Articulos> Articulos { get; set; }

	public DbSet<TrabajosDetalle> TrabajosDetalles { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Articulos>().HasData(new List<Articulos>()
		{
			new Articulos() {ArticuloId = 1,Descripcion = "Multimetro", Costo = 2000, Precio = 5000, Existencia = 15},
			new Articulos() {ArticuloId = 2,Descripcion = "Cable de redes", Costo = 250, Precio = 500, Existencia = 25},
			new Articulos() {ArticuloId = 3,Descripcion = "Pinzas", Costo = 500, Precio = 750, Existencia = 50}
		});
	}
}
