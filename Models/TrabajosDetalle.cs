using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class TrabajosDetalle
{
	[Key]
	public int DetalleId { get; set; }

	[ForeignKey("Trabajo")]
	public int TrabajoId { get; set; }
	public Trabajos? Trabajo { get; set; }


	[ForeignKey("Articulo")]
	public int ArticuloId { get; set; }
	public Articulos? Articulo { get; set; }

	[Required(ErrorMessage = "Ingrese una cantidad valida")]
	[Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
	public int Cantidad { get; set; }


	[Required(ErrorMessage = "Ingrese un precio valido")]
	[Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
	public double Precio { get; set; }


	[Required(ErrorMessage = "Ingrese un costo valido")]
	[Range(1, int.MaxValue, ErrorMessage = "El costo debe ser mayor que 0")]
	public double Costo { get; set; }
}
