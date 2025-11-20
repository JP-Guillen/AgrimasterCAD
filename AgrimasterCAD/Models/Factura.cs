using System.ComponentModel.DataAnnotations;

namespace AgrimasterCAD.Models;

public class Factura
{
    [Key]
    public int FacturaId { get; set; }

    public int SolicitudId { get; set; }
    public double Total { get; set; }
    public DateTime FechaEmision { get; set; }

    public string ArchivoFactura { get; set; }

    public Solicitud Solicitud { get; set; }
}
