using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }


    [Required(ErrorMessage = "Por favor ingrese el nombre del técnico")]
	[RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$", ErrorMessage = "El nombre solo debe contener letras.")]

	public string? NombreTecnico { get; set; }

    [Required(ErrorMessage = "Por favor ingrese el sueldo del técnico")]
    [Range(0.01, float.MaxValue, ErrorMessage = "El sueldo debe ser mayor que 0")]
    public float? SueldoHora { get; set; }

    [ForeignKey("TipoTecnico")]
    [Range(1, int.MaxValue, ErrorMessage = "Por favor, seleccione una opción válida")]
    public int TipoTecnicoId { get; set; }

    public TiposTecnicos? TipoTecnico { get; set; }
}