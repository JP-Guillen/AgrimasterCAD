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

    public virtual Solicitud Solicitud { get; set; }
}
