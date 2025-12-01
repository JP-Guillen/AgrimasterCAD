using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgrimasterCAD.Models;

public class SolicitudSeguimientos
{
    [Key]
    public int SolicitudSeguimientoId { get; set; }
    public int SolicitudId { get; set; }
    public string Comentario { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;

    [ForeignKey(nameof(SolicitudId))]
    public Solicitudes Solicitud { get; set; }
}
