namespace AgrimasterCAD.Models;

public class Factura
{
    [Key]
    public int FacturaId { get; set; }
    public int SolicitudId { get; set; }
    public double Total { get; set; }
    public DateTime FechaEmisión { get; set; }
    public string ArchivoFactura { get; set; }

    public virtual Solicitud Solicitud { get; set; }
}
