using Agendamento.Api.Dto;
using Agendamento.Application.Dto;
using Agendamento.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Api.Controllers;

[ApiController]
[Route("api/agendamentos")]
public class AgendamentoController : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService;

    public AgendamentoController(IAgendamentoService agendamentoService)
    {
        _agendamentoService = agendamentoService;
    }

    
    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<List<AgendamentoResponseDto>>> ListarAsync()
    {
        var agendamentos = await _agendamentoService.ListarAsync();
        return Ok(agendamentos);
    }

    
    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<ActionResult<AgendamentoResponseDto>> CriarAsync(AgendamentoRequestDto request)
    {
        try
        {
            var agendamento = await _agendamentoService.CriarAsync(request);
            return CreatedAtAction("Listar", agendamento);
        }
        catch (Exception e)
        {
            return  Problem(
                  e.Message,
                  statusCode: StatusCodes.Status400BadRequest
                  );
        }
    }

    
    [HttpPut("concluir/{id}")]
    [ProducesResponseType(typeof(AgendamentoResponseDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AgendamentoResponseDto>> ConcluirAsync(int id)
    {
        try
        {
            var agendamento = await _agendamentoService.ConcluirAsync(id);
            return Ok(agendamento);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpPut("cancelar/{id}")]
    [ProducesResponseType(typeof(AgendamentoResponseDto),StatusCodes.Status200OK)]
    public async Task<ActionResult<AgendamentoResponseDto>> CancelarAsync(int id)
    {
        try
        {
            var agendamento = await _agendamentoService.CancelarAsync(id);
            return Ok(agendamento);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AgendamentoResponseDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AgendamentoResponseDto>> BuscarPorIdAsync(int id)
    {
        try
        {
            var  agendamento = await _agendamentoService.BuscarPorIdAsync(id);
            return Ok(agendamento);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(AgendamentoResponseDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletarAsync(int id)
    {
        try
        {
            await  _agendamentoService.DeletarAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("atualizar/{id}")]
    [ProducesResponseType(typeof(AgendamentoResponseDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AgendamentoResponseDto>> AtualizarAsync(int id, AgendamentoRequestDto request)
    {
        try
        {
            var agendamento = await _agendamentoService.AtualizarAsync(id, request);
            return Ok(agendamento);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}