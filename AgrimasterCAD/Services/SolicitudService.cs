using AgrimasterCAD.Data;
using AgrimasterCAD.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class SolicitudService
{
    private readonly ApplicationDbContext _context;

    public SolicitudService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Crear nueva solicitud
    public async Task<SolicitudPlano> CrearSolicitudAsync(SolicitudPlano solicitud)
    {
        _context.SolicitudesPlano.Add(solicitud);
        await _context.SaveChangesAsync();
        return solicitud;
    }

    // Obtener por ID
    public async Task<SolicitudPlano?> ObtenerSolicitudAsync(int id)
    {
        return await _context.SolicitudesPlano
            .Include(s => s.Documentos)
            .Include(s => s.Actividades)
            .Include(s => s.Agrimensor)
            .Include(s => s.Cliente)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    // Listado para cliente
    public async Task<List<SolicitudPlano>> ObtenerSolicitudesClienteAsync(string userId)
    {
        return await _context.SolicitudesPlano
            .Where(s => s.ClienteId == userId)
            .OrderByDescending(s => s.FechaSolicitud)
            .ToListAsync();
    }

    // Listado para agrimensor (pendientes)
    public async Task<List<SolicitudPlano>> ObtenerPendientesAsync()
    {
        return await _context.SolicitudesPlano
            .Where(s => s.Estado == EstadoSolicitud.Pendiente)
            .OrderBy(s => s.FechaSolicitud)
            .ToListAsync();
    }

    // Aceptar solicitud
    public async Task AceptarSolicitudAsync(int solicitudId, string agrimensorId)
    {
        var solicitud = await _context.SolicitudesPlano.FindAsync(solicitudId);
        if (solicitud == null) return;

        solicitud.AgrimensorId = agrimensorId;
        solicitud.Estado = EstadoSolicitud.Asignada;
        solicitud.FechaAceptacion = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    // Registrar actividad
    public async Task RegistrarActividadAsync(EstadoActividad actividad)
    {
        _context.EstadoActividades.Add(actividad);
        await _context.SaveChangesAsync();
    }

    // Agregar documento
    public async Task AgregarDocumentoAsync(SolicitudDocumento documento)
    {
        _context.SolicitudDocumentos.Add(documento);
        await _context.SaveChangesAsync();
    }

    // Guardar plano final
    public async Task SubirPlanoFinalAsync(PlanoFinal plano)
    {
        _context.PlanosFinales.Add(plano);
        await _context.SaveChangesAsync();
    }
}
