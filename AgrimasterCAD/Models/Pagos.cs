using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgrimasterCAD.Models;

public class Pagos
{
    [Key]
    public int PagoId { get; set; }
    public int SolicitudId { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public decimal Monto { get; set; }
    public string Metodo { get; set; }
    public string? ReciboRuta { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;

    [ForeignKey(nameof(SolicitudId))]
    public Solicitudes Solicitud { get; set; }
}
