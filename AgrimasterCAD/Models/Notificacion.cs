namespace AgrimasterCAD.Models;

public class Notificacion
{
    [Key]
    public int NotificacionId { get; set; }
    public int UsuarioId { get; set; }
    public string Titulo { get; set; }
    public string Contenido { get; set; }
    public DateTime FechaNotificación { get; set; }
    public bool Leída { get; set; }

    public virtual Usuario Usuario { get; set; }
}
