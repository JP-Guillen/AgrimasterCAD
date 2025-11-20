using AgrimasterCAD.DAL;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class PagoService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int pagoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pagos.AnyAsync(p => p.PagoId == pagoId);
    }

    private async Task<bool> Insertar(Pago pago)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Pagos.Add(pago);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Pago pago)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(pago);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Pago pago)
    {
        if (!await Existe(pago.PagoId))
            return await Insertar(pago);
        else
            return await Modificar(pago);
    }

    public async Task<Pago?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pagos
            .Include(p => p.Solicitud)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PagoId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pagos
            .Where(p => p.PagoId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Pago>> Listar(Expression<Func<Pago, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pagos
            .Where(criterio)
            .Include(p => p.Solicitud)
            .AsNoTracking()
            .ToListAsync();
    }
}
