using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hamlet_GarciaAP1_P2.Models;

public class NavesEspaciales
{
    [Key]
    public int NaveId { get; set; }

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public int Costo { get; set; }
}

