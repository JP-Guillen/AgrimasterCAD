using AgrimasterCAD.DAL;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class NotificacionService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Notificaciones.AnyAsync(n => n.NotificacionId == id);
    }

    private async Task<bool> Insertar(Notificacion notificacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Notificaciones.Add(notificacion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Notificacion notificacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(notificacion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Notificacion notificacion)
    {
        if (!await Existe(notificacion.NotificacionId))
            return await Insertar(notificacion);
        else
            return await Modificar(notificacion);
    }

    public async Task<Notificacion?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Notificaciones
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.NotificacionId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Notificaciones
            .Where(n => n.NotificacionId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Notificacion>> Listar(Expression<Func<Notificacion, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Notificaciones
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
