using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgrimasterCAD.Models;

public class Planos
{
    [Key]
    public int PlanoId { get; set; }
    public int SolicitudId { get; set; }
    public string RutaArchivo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;

    [ForeignKey(nameof(SolicitudId))]
    public Solicitudes? Solicitud { get; set; }
}
