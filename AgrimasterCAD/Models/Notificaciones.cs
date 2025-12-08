using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgrimasterCAD.Data;

namespace AgrimasterCAD.Models;

public class Notificaciones
{
    [Key]
    public int NotificacionId { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public bool Leida { get; set; } = false;
    public DateTime Fecha { get; set; } = DateTime.Now;

    [ForeignKey(nameof(UsuarioId))]
    public ApplicationUser? Usuario { get; set; }
}
