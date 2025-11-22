using AgrimasterCAD.Data;
using AgrimasterCAD.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class NotificacionService
{
    private readonly ApplicationDbContext _context;

    public NotificacionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CrearAsync(Notificacion noti)
    {
        _context.Notificaciones.Add(noti);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Notificacion>> ObtenerPorUsuarioAsync(string userId)
    {
        return await _context.Notificaciones
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.Fecha)
            .ToListAsync();
    }

    public async Task MarcarLeidaAsync(int id)
    {
        var noti = await _context.Notificaciones.FindAsync(id);
        if (noti == null) return;

        noti.Leida = true;
        await _context.SaveChangesAsync();
    }

    public async Task MarcarTodasLeidasAsync(string userId)
    {
        var notis = await _context.Notificaciones
            .Where(n => n.UserId == userId)
            .ToListAsync();

        foreach (var n in notis)
            n.Leida = true;

        await _context.SaveChangesAsync();
    }
}
