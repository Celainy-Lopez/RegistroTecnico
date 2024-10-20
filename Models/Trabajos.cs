using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class Trabajos
{
	[Key]
	public int TrabajoId { get; set; }
	public ICollection<TrabajosDetalle> TrabajosDetalle { get; set; } = new List<TrabajosDetalle>();

	[Required(ErrorMessage ="Ingrese una descripcion valida")]
	[RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$", ErrorMessage = "La descripción solo debe contener letras.")]

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

	public Prioridades Prioridad { get; set; }
	[ForeignKey("Prioridad")]
	[Range(1, int.MaxValue, ErrorMessage ="Por favor, seleccione una opcion valida")]
	public int PrioridadId { get; set; }

}
