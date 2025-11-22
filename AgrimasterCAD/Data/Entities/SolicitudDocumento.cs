namespace AgrimasterCAD.Data.Entities;

public class SolicitudDocumento
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }
    public SolicitudPlano Solicitud { get; set; } = default!;

    public string Nombre { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
}
