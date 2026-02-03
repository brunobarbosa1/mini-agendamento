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
        
        var agendamentoEntity = await _agendamentoRepository.AdicionarAsync(agendamento);
        await _agendamentoRepository.SalvarAsync(agendamento);
        
        return AgendamentoMapper.ToResponse(agendamentoEntity);
    }

    
    public async Task<AgendamentoResponseDto> ConcluirAsync(int idAgendamento)
    {
        var agendamento = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);

        if (agendamento == null)
        {
            throw new Exception("Agendamento não encontrado!");
        }

        if (agendamento.Status == StatusAgendamento.CANCELADO)
        {
            throw new("Apenas Status como CRIADO podem ser concluido!");
        }

        agendamento.Status = StatusAgendamento.CONCLUIDO;
        await _agendamentoRepository.SalvarAsync(agendamento);
        
        return AgendamentoMapper.ToResponse(agendamento);
    }

    
    public async Task<AgendamentoResponseDto> CancelarAsync(int idAgendamento)
    {
        var agendamento = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);

        if (agendamento == null)
        {
            throw new Exception("Agendamento não encontrado!");
        }

        if (agendamento.Status == StatusAgendamento.CONCLUIDO)
        { 
            throw new("Apenas Status como CRIADO podem ser cancelado!");
        }
        
        agendamento.Status = StatusAgendamento.CANCELADO;
        await _agendamentoRepository.SalvarAsync(agendamento);
        return AgendamentoMapper.ToResponse(agendamento);
    }
    
    
    public async Task<AgendamentoResponseDto?> BuscarPorIdAsync(int idAgendamento)
    {
        var agendandamento  = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);
        if (agendandamento == null)
        {
            throw new Exception("Agendamento não encontrado!");
        }
        return AgendamentoMapper.ToResponse(agendandamento);
    }
    

    public async Task DeletarAsync(int idAgendamento)
    {
        var agendamento = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);
        if (agendamento == null)
        {
            throw new Exception("Agendamento não encontrado para deletar!");
        }
        
        await _agendamentoRepository.DeletarAsync(agendamento);
    }

    public async Task<AgendamentoResponseDto> AtualizarAsync(int idAgendamento, AgendamentoRequestDto request)
    {
        var agendamento  = await _agendamentoRepository.BuscarPorIdAsync(idAgendamento);
        if (agendamento == null)
        {
            throw new Exception("Agendamento não encontrado!");
        }
        
        agendamento.Data = request.Data;
        agendamento.HoraInicio = request.HoraInicio;
        agendamento.HoraFim = request.HoraFim;
        agendamento.AtualizadoEm = DateTime.Now;
        
        await _agendamentoRepository.SalvarAsync(agendamento);
        return AgendamentoMapper.ToResponse(agendamento);
    }


    // Controle de fluxo na criação de um agendamento sem conflito
    private static bool ExisteConflito(TimeOnly novoInicio, TimeOnly novoFim, IEnumerable<AgendamentoEntity> existentes)
    {
        return existentes
            .Any(a => novoInicio < a.HoraFim && novoFim > a.HoraInicio);
    }
}