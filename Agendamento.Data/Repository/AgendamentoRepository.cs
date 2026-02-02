using Agendamento.Data.Data;
using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Data.Repository;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly AppDbContext _context;
    
    public AgendamentoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<AgendamentoEntity> CriarAsync(AgendamentoEntity agendamento)
    {
        await  _context.Agendamentos.AddAsync(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }

    public async Task<AgendamentoEntity> SalvarAsync(AgendamentoEntity agendamento)
    {
        await _context.SaveChangesAsync();
        return agendamento;
    }

    public async Task<List<AgendamentoEntity>> ListarAsync()
    {
        return await _context.Agendamentos.ToListAsync();
    }

    public async Task<AgendamentoEntity?> BuscarPorIdAsync(int id)
    {
        return await _context.Agendamentos.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<AgendamentoEntity>> BuscarPorDataAsync(DateOnly data)
    {
        return await _context.Agendamentos.Where(a => a.Data == data).ToListAsync();
    }

    public async Task DeletarAsync(AgendamentoEntity agendamento)
    {
        _context.Agendamentos.Remove(agendamento);
        await _context.SaveChangesAsync();
    }
}