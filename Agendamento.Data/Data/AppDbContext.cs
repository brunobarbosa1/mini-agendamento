using Microsoft.EntityFrameworkCore;

namespace Agendamento.Data.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Domain.Entities.AgendamentoEntity> Agendamentos { get; set; }
}