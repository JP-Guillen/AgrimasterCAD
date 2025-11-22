namespace AgrimasterCAD.Data.Entities;

public class Pago
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }
    public SolicitudPlano Solicitud { get; set; } = default!;

    public decimal Monto { get; set; }

    public MetodoPago Metodo { get; set; }
    public EstadoPago Estado { get; set; }

    public DateTime? FechaPago { get; set; }
    public string? UrlReciboTransferencia { get; set; }
}

public enum MetodoPago
{
    Tarjeta,
    Transferencia,
    Efectivo
}

public enum EstadoPago
{
    Pendiente,
    EnRevision,
    Confirmado,
    Rechazado
}
