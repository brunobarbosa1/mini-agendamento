using Agendamento.Domain.Enum;

namespace Agendamento.Api.Dto;

public record AgendamentoRequestDto(
     DateOnly Data,
     TimeOnly HoraInicio, 
     TimeOnly HoraFim
     );