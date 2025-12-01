using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgrimasterCAD.Data;

namespace AgrimasterCAD.Models;

public class MetodosPago
{
    [Key]
    public int MetodoPagoId { get; set; }
    public string UsuarioId { get; set; }
    public string Tipo { get; set; }
    public string Ultimos4Digitos { get; set; }

    [ForeignKey(nameof(UsuarioId))]
    public ApplicationUser Usuario { get; set; }
}
