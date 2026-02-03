using Agendamento.Api.Dto;
using Agendamento.Domain.Entities;

namespace Agendamento.Application.Service;

public interface IAgendamentoService
{
    Task<AgendamentoResponseDto> CriarAsync(AgendamentoRequestDto request);
    Task<AgendamentoResponseDto> ConcluirAsync(int idAgendamento);
    Task<AgendamentoResponseDto> CancelarAsync(int idAgendamento);
    Task<List<AgendamentoResponseDto>> ListarAsync();
    Task<AgendamentoResponseDto?> BuscarPorIdAsync(int idAgendamento);
    Task DeletarAsync(int idAgendamento);
    Task<AgendamentoResponseDto>  AtualizarAsync(int idAgendamento, AgendamentoRequestDto request);
}
