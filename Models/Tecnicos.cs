using System.ComponentModel.DataAnnotations;
namespace RegistroTecnico.Models
{
    public class Tecnicos
    {
        [Key]
        public int tecnicoId { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el nombre del técnico")]
        public string? nombreTecnico { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el sueldo del técnico")]
        [Range(0.01, float.MaxValue, ErrorMessage = "El sueldo debe ser mayor que 0")]
        public float? sueldoHora { get; set; }
    }
}
