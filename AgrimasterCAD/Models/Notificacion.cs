using System.ComponentModel.DataAnnotations;

namespace AgrimasterCAD.Models;

public class Notificacion
{
    [Key]
    public int NotificacionId { get; set; }

    public int UsuarioId { get; set; }
    public string Titulo { get; set; }
    public string Contenido { get; set; }
    public DateTime FechaNotificacion { get; set; }
    public bool Leida { get; set; }

    public Usuario Usuario { get; set; }
}

