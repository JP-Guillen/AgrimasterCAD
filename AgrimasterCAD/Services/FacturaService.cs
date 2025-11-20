using AgrimasterCAD.DAL;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class FacturaService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int facturaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Facturas.AnyAsync(f => f.FacturaId == facturaId);
    }

    private async Task<bool> Insertar(Factura factura)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Facturas.Add(factura);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Factura factura)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(factura);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Factura factura)
    {
        if (!await Existe(factura.FacturaId))
            return await Insertar(factura);
        else
            return await Modificar(factura);
    }

    public async Task<Factura?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Include(f => f.Solicitud)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FacturaId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Where(f => f.FacturaId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Factura>> Listar(Expression<Func<Factura, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Where(criterio)
            .Include(f => f.Solicitud)
            .AsNoTracking()
            .ToListAsync();
    }
}
