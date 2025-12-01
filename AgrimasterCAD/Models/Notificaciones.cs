using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgrimasterCAD.Data;

namespace AgrimasterCAD.Models;

public class Notificaciones
{
    [Key]
    public int NotificacionId { get; set; }
    public string UsuarioId { get; set; }
    public string Titulo { get; set; }
    public string Mensaje { get; set; }
    public string Tipo { get; set; }
    public bool Leida { get; set; } = false;
    public DateTime Fecha { get; set; } = DateTime.Now;

    [ForeignKey(nameof(UsuarioId))]
    public ApplicationUser Usuario { get; set; }
}
