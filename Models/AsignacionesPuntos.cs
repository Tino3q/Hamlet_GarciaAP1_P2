using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hamlet_GarciaAP1_P2.Models;

public class AsignacionesPuntos
{

    [Key]
    public int IdAsignacion { get; set; }

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public int EstudianteId { get; set; }

    public int TotalPuntos { get; set; }

    [ForeignKey("EstudianteId")]
    public Estudiantes? Estudiante { get; set; }

    public List<DetalleAsignacion> Detalle { get; set; } = new();

}