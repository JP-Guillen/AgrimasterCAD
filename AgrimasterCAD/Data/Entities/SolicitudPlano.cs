using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgrimasterCAD.Data.Entities;

public class SolicitudPlano
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;

    public string ClienteId { get; set; } = default!;
    public ApplicationUser Cliente { get; set; } = default!;

    public string? AgrimensorId { get; set; }
    public ApplicationUser? Agrimensor { get; set; }

    public string Titulo { get; set; } = string.Empty;
    public double Superficie { get; set; }

    public double UbicacionLat { get; set; }
    public double UbicacionLng { get; set; }

    public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;

    public decimal CostoEstimado { get; set; }
    public decimal? CostoFinal { get; set; }

    public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;
    public DateTime? FechaAceptacion { get; set; }
    public DateTime? FechaFinalizacion { get; set; }

    public List<SolicitudDocumento> Documentos { get; set; } = new();
    public List<EstadoActividad> Actividades { get; set; } = new();
}

public enum EstadoSolicitud
{
    Pendiente,
    Asignada,
    EnProceso,
    Finalizada,
    Cancelada
}
