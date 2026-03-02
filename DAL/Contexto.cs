using Microsoft.EntityFrameworkCore;
using Hamlet_GarciaAP1_P2.Models; 

namespace Hamlet_GarciaAP1_P2.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<NavesEspaciales> NavesEspaciales { get; set; }
}