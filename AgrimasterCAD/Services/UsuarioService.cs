using AgrimasterCAD.DAL;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class UsuarioService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Usuarios.AnyAsync(u => u.UsuarioId == id);
    }

    private async Task<bool> Insertar(Usuario usuario)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Usuarios.Add(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Usuario usuario)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Usuario usuario)
    {
        if (!await Existe(usuario.UsuarioId))
            return await Insertar(usuario);
        else
            return await Modificar(usuario);
    }

    public async Task<Usuario?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
            .Include(u => u.Notificaciones)
            .Include(u => u.Solicitudes)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UsuarioId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
            .Where(u => u.UsuarioId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Usuario>> Listar(Expression<Func<Usuario, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
            .Where(criterio)
            .Include(u => u.Notificaciones)
            .Include(u => u.Solicitudes)
            .AsNoTracking()
            .ToListAsync();
    }
}
