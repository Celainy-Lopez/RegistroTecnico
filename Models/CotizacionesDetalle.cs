using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class CotizacionesDetalle
{
    [Key]
    public int DetalleId { get; set; }

    [ForeignKey("CotizacionId")]
    [InverseProperty("CotizacionesDetalle")]
    public virtual Cotizaciones Cotizacion { get; set; } = null!;
    public int CotizacionId { get; set; }

    [ForeignKey("Articulo")]
    public int ArticuloId { get; set; }
    public Articulos? Articulo { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
    public double Precio { get; set; }

}