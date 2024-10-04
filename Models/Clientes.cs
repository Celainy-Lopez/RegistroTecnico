using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;

public class Clientes
{
	[Key]
	public int ClienteId { get; set; }

	[Required(ErrorMessage ="Ingrese un nombre valido")]
	public string Nombres { get; set; }

	[Required(ErrorMessage ="Ingrese un whatsApp valido")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe tener exactamente 10 dígitos.")]
    public string? WhatsApp {  get; set; }

}
