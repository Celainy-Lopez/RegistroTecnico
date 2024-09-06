using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;

public class TiposTecnicos
{
	[Key]
	public int TipoTecnicoId { get; set; }

	[Required(ErrorMessage = "Por favor ingrese una descripción valida")]
	public string Descripcion { get; set; }
}
