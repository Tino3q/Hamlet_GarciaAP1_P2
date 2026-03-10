using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hamlet_GarciaAP1_P2.Models;

public class DetalleAsignacion
{
    [Key]
    public int IdDetalle { get; set; }

    public int IdAsignacion { get; set; }

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public int TipoPuntoId { get; set; }

    public int CantidadPuntos { get; set; }

    [ForeignKey("IdAsignacion")]
    public AsignacionesPuntos? Asignacion { get; set; }

    [ForeignKey("TipoPuntoId")]
    public TiposPuntos? TipoPunto { get; set; }
}