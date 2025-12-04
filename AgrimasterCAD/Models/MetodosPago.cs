using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgrimasterCAD.Data;

namespace AgrimasterCAD.Models;

public class MetodosPago
{
    [Key]
    public int MetodoPagoId { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Ultimos4Digitos { get; set; } = string.Empty;

    [ForeignKey(nameof(UsuarioId))]
    public ApplicationUser? Usuario { get; set; }
}
