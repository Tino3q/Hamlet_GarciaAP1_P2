using Hamlet_GarciaAP1_P2.DAL;
using Hamlet_GarciaAP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hamlet_GarciaAP1_P2.Services;

public class NavesEspacialesService(IDbContextFactory<Contexto> DbFactory)
{
   
    public async Task<List<NavesEspaciales>> Listar(Expression<Func<NavesEspaciales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.NavesEspaciales
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}