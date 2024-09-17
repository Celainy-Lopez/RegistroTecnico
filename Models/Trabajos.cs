using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class Trabajos
{
	[Key]
	public int TrabajoId { get; set; }

	[Required(ErrorMessage ="Ingrese una descripcion valida")]
	public string DescripcionTrabajo {  get; set; }

        [Required(ErrorMessage = "Ingrese un monto valido")]
        [Range(0.01, float.MaxValue, ErrorMessage = "El sueldo debe ser mayor que 0")]
        public float? Monto { get; set; }

	public DateTime? Fecha { get; set; } = DateTime.Now;

	public Clientes Cliente { get; set; }
	[ForeignKey("Cliente")]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor, seleccione una opción válida")]
        public int ClienteId { get; set; }


	public Tecnicos Tecnico { get; set; }
	[ForeignKey("Tecnico")]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor, seleccione una opción válida")]
        public int TecnicoId { get; set; }

}
