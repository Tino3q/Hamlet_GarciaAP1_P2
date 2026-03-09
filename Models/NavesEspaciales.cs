using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Hamlet_GarciaAP1_P2.Models;

public class NavesEspaciales
{
    [Key]
    public int NaveId { get; set; }

    [Required(ErrorMessage = "Este Campo Es Requerido")]

    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public int Costo { get; set; }
    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public string Modelo { get; set; } = string.Empty;
    [Required(ErrorMessage = "Este Campo Es Requerido")]

    public DateTime FechaCreacion { get; set; } = DateTime.Now;


}

