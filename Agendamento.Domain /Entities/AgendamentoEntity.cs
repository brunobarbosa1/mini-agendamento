using Agendamento.Domain.Enum;

namespace Agendamento.Domain.Entities;

public class AgendamentoEntity
{
    public int Id { get; set; }
    public DateOnly Data { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFim { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
    public StatusAgendamento Status { get; set; }

    public AgendamentoEntity()
    {
    }

    public AgendamentoEntity(DateOnly data, TimeOnly horaInicio, TimeOnly horaFim, DateTime criadoEm, DateTime atualizadoEm,
        StatusAgendamento status)
    {
        Data = data;
        HoraInicio = horaInicio;
        HoraFim = horaFim;
        CriadoEm = criadoEm;
        AtualizadoEm = atualizadoEm;
        Status = status;
    }
    
    public static void ValidarIntervalo(TimeOnly inicio, TimeOnly fim)
    {
        if (fim < inicio)
        {
            throw new Exception("Data fim não pode ser menor que a Data de início");
        }
    }
}