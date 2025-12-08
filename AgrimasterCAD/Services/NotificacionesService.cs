using AgrimasterCAD.Data;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class NotificacionesService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task Crear(string usuarioId, string titulo, string mensaje, string tipo = "info")
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var notificacion = new Notificaciones
        {
            UsuarioId = usuarioId,
            Titulo = titulo,
            Mensaje = mensaje,
            Tipo = tipo,
            Fecha = DateTime.Now
        };

        contexto.Notificaciones.Add(notificacion);
        await contexto.SaveChangesAsync();
    }

    public async Task Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var notificacion = await contexto.Notificaciones.FindAsync(id);
        if (notificacion != null)
        {
            contexto.Notificaciones.Remove(notificacion);
            await contexto.SaveChangesAsync();
        }
    }

    public async Task EliminarTodas(string userId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var lista = await contexto.Notificaciones
            .Where(n => n.UsuarioId == userId)
            .ToListAsync();

        contexto.Notificaciones.RemoveRange(lista);
        await contexto.SaveChangesAsync();
    }

    public async Task<List<Notificaciones>> Listar(string usuarioId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Notificaciones
            .Where(n => n.UsuarioId == usuarioId)
            .OrderByDescending(n => n.Fecha)
            .ToListAsync();
    }

    public async Task MarcarLeida(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var notificacion = await contexto.Notificaciones.FirstOrDefaultAsync(n => n.NotificacionId == id);
        if (notificacion == null) return;

        notificacion.Leida = true;
        await contexto.SaveChangesAsync();
    }

    public async Task<int> ContarNoLeidas(string usuarioId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Notificaciones.CountAsync(n => n.UsuarioId == usuarioId && !n.Leida);
    }

    public async Task MarcarTodasLeidas(string userId)
        {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var lista = await contexto.Notificaciones
            .Where(n => n.UsuarioId == userId && !n.Leida)
            .ToListAsync();

        foreach (var notificacion in lista)
            notificacion.Leida = true;

            await contexto.SaveChangesAsync();
        }
    }
}
