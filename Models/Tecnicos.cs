using System.ComponentModel.DataAnnotations;
namespace RegistroTecnico.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el nombre del técnico")]
        public string? NombreTecnico { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el sueldo del técnico")]
        [Range(0.01, float.MaxValue, ErrorMessage = "El sueldo debe ser mayor que 0")]
        public float? SueldoHora { get; set; }
    }
}
