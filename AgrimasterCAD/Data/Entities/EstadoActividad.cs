namespace AgrimasterCAD.Data.Entities;

public class EstadoActividad
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }
    public SolicitudPlano Solicitud { get; set; } = default!;

    public EstadoTrabajo Estado { get; set; }
    public string Comentario { get; set; } = string.Empty;

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}

public enum EstadoTrabajo
{
    Agendado,
    EnCurso,
    Finalizado
}
