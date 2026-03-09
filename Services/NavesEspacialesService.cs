using Hamlet_GarciaAP1_P2.DAL;
using Hamlet_GarciaAP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hamlet_GarciaAP1_P2.Services;

public class NavesEspacialesService(IDbContextFactory<Contexto> dbFactory)
{
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.NavesEspaciales.AnyAsync(n => n.NaveId == id);
    }

    private async Task<bool> Insertar(NavesEspaciales nave)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.NavesEspaciales.Add(nave);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(NavesEspaciales nave)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Update(nave);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(NavesEspaciales nave)
    {
        if (!await Existe(nave.NaveId))
            return await Insertar(nave);
        else
            return await Modificar(nave);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var nave = await contexto.NavesEspaciales
            .Where(n => n.NaveId == id)
            .ExecuteDeleteAsync();
        return nave > 0;
    }

    public async Task<NavesEspaciales?> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.NavesEspaciales
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.NaveId == id);
    }

    public async Task<List<NavesEspaciales>> Listar(Expression<Func<NavesEspaciales, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.NavesEspaciales
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}