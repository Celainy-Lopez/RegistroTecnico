using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class Cotizaciones
{
    [Key]
    public int CotizacionId { get; set; }

    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$", ErrorMessage = "La observación solo debe contener letras.")]

    public string Observaciones { get; set; }

    public double Monto { get; set; }

    [ForeignKey("Cliente")]
    [Range(1, int.MaxValue, ErrorMessage = "Por favor, seleccione una opción válida")]

    public int ClienteId { get; set; }
    public Clientes? Cliente { get; set; }

    [InverseProperty("Cotizacion")]
    public virtual ICollection<CotizacionesDetalle> CotizacionesDetalle { get; set; } = new List<CotizacionesDetalle>();
}