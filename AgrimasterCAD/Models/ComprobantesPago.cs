using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgrimasterCAD.Models;

public class ComprobantesPago
{
    [Key]
    public int ComprobantePagoId { get; set; }
    public int SolicitudId { get; set; }
    public decimal Monto { get; set; }
    public string Metodo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;

    [ForeignKey(nameof(SolicitudId))]
    public Solicitudes? Solicitud { get; set; }
}
