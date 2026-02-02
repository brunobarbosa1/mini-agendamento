using Agendamento.Api.Dto;
using Agendamento.Application.Mappers;
using Agendamento.Domain.Enum;
using Agendamento.Data.Repository;
using Agendamento.Domain.Entities;

namespace Agendamento.Application.Service;

public class AgendamentoService : IAgendamentoService
{
    
    private readonly IAgendamentoRepository _agendamentoRepository;

    public AgendamentoService(IAgendamentoRepository agendamentoRepository)
    {
        _agendamentoRepository = agendamentoRepository;
    }
    
    
    public async Task<List<AgendamentoResponseDto>> ListarAsync()
    {
        var agendamentos = await _agendamentoRepository.ListarAsync();
        var response = new List<AgendamentoResponseDto>();

        foreach (var agendamento in agendamentos)
        {
            response.Add(AgendamentoMapper.ToResponse(agendamento));
        }
        return response;
    }
    
    
    public async Task<AgendamentoResponseDto> CriarAsync(AgendamentoRequestDto request)
    {
        AgendamentoEntity.ValidarIntervalo(request.HoraInicio, request.HoraFim);

        List<AgendamentoEntity> agendamentos = await _agendamentoRepository.BuscarPorDataAsync(request.Data);
        
        if (ExisteConflito(request.HoraInicio, request.HoraFim, agendamentos))
        {
            throw new Exception("Existe um agendamento no horário registrado. Tente outro horário.");
        }
        
        var agendamento = AgendamentoMapper.ToEntity(request);
        
        agendamento.CriadoEm = DateTime.Now;
        agendamento.AtualizadoEm = DateTime.Now;
        agendamento.Status = StatusAgendamento.CRIADO;
        
        var salvar = await _agendamentoRepository.CriarAsync(agendamento);
        
        return AgendamentoMapper.ToResponse(salvar);
    }

    
    public async Task<AgendamentoResponseDto> ConcluirAsync(int idAgendamento)
    {
        var agendamento = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);

        if (agendamento == null)
        {
            throw new Exception("Usuário não encontrado!");
        }
        agendamento.Status = StatusAgendamento.CONCLUIDO;
        var salvar = await _agendamentoRepository.SalvarAsync(agendamento);
        
        return AgendamentoMapper.ToResponse(salvar);
    }

    
    public async Task<AgendamentoResponseDto> CancelarAsync(int idAgendamento)
    {
        var agendamento = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);

        if (agendamento == null)
        {
            throw new Exception("Usuário não encontrado!");
        }
        agendamento.Status = StatusAgendamento.CANCELADO;
        var salvar = await _agendamentoRepository.SalvarAsync(agendamento);
        
        return AgendamentoMapper.ToResponse(salvar);
    }
    
    
    public async Task<AgendamentoResponseDto?> BuscarPorIdAsync(int idAgendamento)
    {
        var agendandamento  = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);
        if (agendandamento == null)
        {
            throw new Exception("Usuário não encontrado!");
        }
        return AgendamentoMapper.ToResponse(agendandamento);
    }

    public async Task DeletarAsync(int idAgendamento)
    {
        var agendamento = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);
        if (agendamento == null)
        {
            throw new Exception("Usuário não encontrado!");
        }
        
        await _agendamentoRepository.DeletarAsync(agendamento);
    }
    
    
    // Controle de fluxo na criação de um agendamento sem conflito
    private static bool ExisteConflito(TimeOnly novoInicio, TimeOnly novoFim, IEnumerable<AgendamentoEntity> existentes)
    {
        return existentes.Any(a => novoInicio < a.HoraFim && novoFim < a.HoraInicio);
    }
}