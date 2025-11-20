using AgrimasterCAD.DAL;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class PlanoService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int planoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Planos.AnyAsync(p => p.PlanoId == planoId);
    }

    private async Task<bool> Insertar(Plano plano)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Planos.Add(plano);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Plano plano)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(plano);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Plano plano)
    {
        if (!await Existe(plano.PlanoId))
            return await Insertar(plano);
        else
            return await Modificar(plano);
    }

    public async Task<Plano?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Planos
            .Include(p => p.Solicitud)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PlanoId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Planos
            .Where(p => p.PlanoId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Plano>> Listar(Expression<Func<Plano, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Planos
            .Where(criterio)
            .Include(p => p.Solicitud)
            .AsNoTracking()
            .ToListAsync();
    }
}
