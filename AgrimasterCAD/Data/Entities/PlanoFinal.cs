namespace AgrimasterCAD.Data.Entities;

public class PlanoFinal
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }
    public SolicitudPlano Solicitud { get; set; } = default!;

    public string UrlPlano { get; set; } = string.Empty;
    public string UrlFactura { get; set; } = string.Empty;

    public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
}
