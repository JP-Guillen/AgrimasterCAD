using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgrimasterCAD.Data;

namespace AgrimasterCAD.Models;

public class Solicitudes
{
    [Key]
    public int SolicitudId { get; set; }
    public string ClienteId { get; set; } = string.Empty;
    public string? AgrimensorId { get; set; }

    public string Estado { get; set; } = "Pendiente";

    [Required(ErrorMessage = "El campo Provincia es obligatorio.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚÑáéíóúñ ]+$", ErrorMessage = "La provincia solo puede contener letras y espacios.")]
    [StringLength(30, ErrorMessage = "La provincia no puede superar los 30 caracteres.")]
    public string Provincia { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Municipio es obligatorio.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚÑáéíóúñ ]+$", ErrorMessage = "El municipio solo puede contener letras y espacios.")]
    [StringLength(30, ErrorMessage = "El municipio no puede superar los 30 caracteres.")]
    public string Municipio { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
    [RegularExpression(pattern: @"^[A-Za-z0-9ÁÉÍÓÚÑáéíóúñ #.,\-]+$", ErrorMessage = "La dirección contiene caracteres no válidos.")]
    [StringLength(120, ErrorMessage = "La dirección no puede superar los 120 caracteres.")]
    public string Direccion { get; set; } = string.Empty;

    public decimal? CostoFinal { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;
    public DateTime? FechaAceptada { get; set; }
    public DateTime? FechaFinalizada { get; set; }
    public DateTime? FechaCancelada { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public ApplicationUser? Cliente { get; set; }

    [ForeignKey(nameof(AgrimensorId))]
    public ApplicationUser? Agrimensor { get; set; }

    public Planos? Plano { get; set; }
    public ComprobantesPago? ComprobantePago { get; set; }

    public List<SolicitudDocumentos> Documentos { get; set; } = new();
    public List<SolicitudSeguimientos> Seguimientos { get; set; } = new();
    public List<Pagos> Pagos { get; set; } = new(); 
}
