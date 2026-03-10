using Hamlet_GarciaAP1_P2.DAL;
using Hamlet_GarciaAP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hamlet_GarciaAP1_P2.Services;

public class AsignacionesService(IDbContextFactory<Contexto> dbFactory)
{
    public async Task<List<Estudiantes>> ListarEstudiantes()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.AsNoTracking().ToListAsync();
    }

    public async Task<List<TiposPuntos>> ListarTiposActivos()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.TiposPuntos.AsNoTracking()
            .Where(t => t.Activo).ToListAsync();
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.AsignacionesPuntos.AnyAsync(a => a.IdAsignacion == id);
    }

    private async Task<bool> Insertar(AsignacionesPuntos asignacion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        asignacion.TotalPuntos = asignacion.Detalle.Sum(d => d.CantidadPuntos);
        contexto.AsignacionesPuntos.Add(asignacion);
        await contexto.SaveChangesAsync();

        var estudiante = await contexto.Estudiantes.FindAsync(asignacion.EstudianteId);
        if (estudiante != null)
        {
            estudiante.BalancePuntos += asignacion.TotalPuntos;
            await contexto.SaveChangesAsync();
        }
        return true;
    }

    private async Task<bool> Modificar(AsignacionesPuntos asignacion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var anterior = await contexto.AsignacionesPuntos
            .Include(a => a.Detalle)
            .FirstOrDefaultAsync(a => a.IdAsignacion == asignacion.IdAsignacion);

        if (anterior == null) return false;

        int puntosAnteriores = anterior.TotalPuntos;
        contexto.DetalleAsignacion.RemoveRange(anterior.Detalle);

        anterior.Fecha = asignacion.Fecha;
        anterior.EstudianteId = asignacion.EstudianteId;
        anterior.Detalle = asignacion.Detalle;
        anterior.TotalPuntos = asignacion.Detalle.Sum(d => d.CantidadPuntos);
        await contexto.SaveChangesAsync();

        var estudiante = await contexto.Estudiantes.FindAsync(asignacion.EstudianteId);
        if (estudiante != null)
        {
            estudiante.BalancePuntos = estudiante.BalancePuntos - puntosAnteriores + anterior.TotalPuntos;
            await contexto.SaveChangesAsync();
        }
        return true;
    }

    public async Task<bool> Guardar(AsignacionesPuntos asignacion)
    {
        if (!await Existe(asignacion.IdAsignacion))
            return await Insertar(asignacion);
        else
            return await Modificar(asignacion);
    }

    public async Task<Estudiantes?> BuscarEstudiante(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EstudianteId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var asignacion = await contexto.AsignacionesPuntos
            .Include(a => a.Detalle)
            .FirstOrDefaultAsync(a => a.IdAsignacion == id);

        if (asignacion == null) return false;

        var estudiante = await contexto.Estudiantes.FindAsync(asignacion.EstudianteId);
        if (estudiante != null)
            estudiante.BalancePuntos -= asignacion.TotalPuntos;

        contexto.AsignacionesPuntos.Remove(asignacion);
        await contexto.SaveChangesAsync();
        return true;
    }

    public async Task<AsignacionesPuntos?> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.AsignacionesPuntos
            .Include(a => a.Detalle)
            .Include(a => a.Estudiante)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.IdAsignacion == id);
    }

    public async Task<List<AsignacionesPuntos>> Listar(Expression<Func<AsignacionesPuntos, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.AsignacionesPuntos
            .Include(a => a.Estudiante)
            .Include(a => a.Detalle)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}