using Agendamento.Domain.Entities;
namespace Agendamento.Data.Repository;

public interface IAgendamentoRepository
{
    Task<AgendamentoEntity> CriarAsync(AgendamentoEntity agendamento);
    Task<AgendamentoEntity> SalvarAsync(AgendamentoEntity agendamento);
    Task<List<AgendamentoEntity>> ListarAsync();
    Task<AgendamentoEntity?> BuscarPorIdAsync(int id);
    Task<List<AgendamentoEntity>> BuscarPorDataAsync(DateOnly data);
    Task DeletarAsync(AgendamentoEntity agendamento);
}