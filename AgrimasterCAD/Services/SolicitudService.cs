using AgrimasterCAD.DAL;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class SolicitudService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int solicitudId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Solicitudes.AnyAsync(s => s.SolicitudId == solicitudId);
    }

    private async Task<bool> Insertar(Solicitud solicitud)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Solicitudes.Add(solicitud);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Solicitud solicitud)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(solicitud);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Solicitud solicitud)
    {
        if (!await Existe(solicitud.SolicitudId))
            return await Insertar(solicitud);
        else
            return await Modificar(solicitud);
    }

    public async Task<Solicitud?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Solicitudes
            .Include(s => s.Usuario)
            .Include(s => s.Agrimensor)
            .Include(s => s.Plano)
            .Include(s => s.Pago)
            .Include(s => s.Factura)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SolicitudId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Solicitudes
            .Where(s => s.SolicitudId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Solicitud>> Listar(Expression<Func<Solicitud, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Solicitudes
            .Where(criterio)
            .Include(s => s.Usuario)
            .Include(s => s.Agrimensor)
            .Include(s => s.Plano)
            .Include(s => s.Pago)
            .Include(s => s.Factura)
            .AsNoTracking()
            .ToListAsync();
    }
}
