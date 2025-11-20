using System.ComponentModel.DataAnnotations;

namespace AgrimasterCAD.Models;
    
public class Usuario
{
    [Key]
    public int UsuarioId { get; set; }

    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Contraseña { get; set; }
    public string Rol { get; set; }
    public string Telefono { get; set; }
    public string Dirección { get; set; }
    public string Provincia { get; set; }
    public string Municipio { get; set; }
    public bool NotificacionesActivadas { get; set; }
    public DateTime FechaRegistro { get; set; }

    public ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
    public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    public Administrador Administrador { get; set; }
    public Agrimensor Agrimensor { get; set; }
}

