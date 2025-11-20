using System.ComponentModel.DataAnnotations;

namespace AgrimasterCAD.Models;

public class Pago
{
    [Key]
    public int PagoId { get; set; }

    public int SolicitudId { get; set; }
    public double Monto { get; set; }
    public string MetodoPago { get; set; }
    public bool PagoConfirmado { get; set; }
    public DateTime FechaPago { get; set; }

    public string ReciboTransferencia { get; set; }

    public Solicitud Solicitud { get; set; }
}
