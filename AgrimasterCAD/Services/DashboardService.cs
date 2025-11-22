using AgrimasterCAD.Data;
using AgrimasterCAD.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class DashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AdminDashboardDto> GetDashboardDataAsync()
    {
        var dto = new AdminDashboardDto
        {
            SolicitudesPendientes = await _context.SolicitudesPlano
                .CountAsync(s => s.Estado == EstadoSolicitud.Pendiente),

            PlanosEnProceso = await _context.SolicitudesPlano
                .CountAsync(s => s.Estado == EstadoSolicitud.EnProceso || s.Estado == EstadoSolicitud.Asignada),

            PagosPendientes = await _context.Pagos
                .CountAsync(p => p.Estado == EstadoPago.Pendiente || p.Estado == EstadoPago.EnRevision),

            AgrimensoresActivos = await _context.Users
                .CountAsync(u => u.NumeroCodia != null) // un agrimensor siempre tiene CODIA
        };

        return dto;
    }
}

public class AdminDashboardDto
{
    public int SolicitudesPendientes { get; set; }
    public int PlanosEnProceso { get; set; }
    public int PagosPendientes { get; set; }
    public int AgrimensoresActivos { get; set; }
}
