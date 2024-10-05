using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;

public class TiposTecnicos
{
	[Key]
	public int TipoTecnicoId { get; set; }

	[Required(ErrorMessage = "Por favor ingrese una descripción valida")]
	[RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$", ErrorMessage = "La descripción solo debe contener letras.")]

	public string Descripcion { get; set; }
}
