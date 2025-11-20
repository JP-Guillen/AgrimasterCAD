using System.ComponentModel.DataAnnotations;

namespace AgrimasterCAD.Models;

public class Agrimensor
{
    [Key]
    public int AgrimensorId { get; set; }

    public int UsuarioId { get; set; }
    public string CodigoCODIA { get; set; }
    public string Teléfono { get; set; }
    public DateTime FechaAsignacion { get; set; }

    public Usuario Usuario { get; set; }
    public ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
}

