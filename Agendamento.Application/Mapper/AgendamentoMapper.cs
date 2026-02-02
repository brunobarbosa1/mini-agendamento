using Agendamento.Api.Dto;
using Agendamento.Domain.Entities;

namespace Agendamento.Application.Mappers;

public class AgendamentoMapper
{
    public static AgendamentoEntity ToEntity(AgendamentoRequestDto dto)
    {
        var agendamentoEntity = new AgendamentoEntity();
        agendamentoEntity.Data = dto.Data;
        agendamentoEntity.HoraInicio = dto.HoraInicio;
        agendamentoEntity.HoraFim = dto.HoraFim;
        return agendamentoEntity;
    }
    

    public static AgendamentoResponseDto ToResponse(AgendamentoEntity entity)
    {
        return new AgendamentoResponseDto(
            entity.Id,
            entity.Data,
            entity.HoraInicio,
            entity.HoraFim,
            entity.CriadoEm,
            entity.AtualizadoEm,
            entity.Status);
    }
}