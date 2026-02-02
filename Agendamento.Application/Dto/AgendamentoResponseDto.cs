using Agendamento.Domain.Enum;

namespace Agendamento.Api.Dto;

public record AgendamentoResponseDto(
    int Id,
    DateOnly Data,
    TimeOnly HoraInicio, 
    TimeOnly HoraFim,
    DateTime CriadoEm,
    DateTime AtualizadoEm,
    StatusAgendamento Status
    );