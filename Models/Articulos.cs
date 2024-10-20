using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;

public class Articulos
{
	[Key]
	public int ArticuloId { get; set; }

	public string Descripcion { get; set; }

	public double Costo { get; set; }

	public double Precio { get; set; }

	public int? Existencia { get; set; }
}
