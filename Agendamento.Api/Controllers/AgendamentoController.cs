using Agendamento.Api.Dto;
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
    public async Task<ActionResult<List<AgendamentoResponseDto>>> ListarAsync()
    {
        var agendamentos = await _agendamentoService.ListarAsync();
        return Ok(agendamentos);
    }

    
    [HttpPost]
    public async Task<ActionResult<AgendamentoResponseDto>> CriarAsync(AgendamentoRequestDto request)
    {
        try
        {
            var agendamento = await _agendamentoService.CriarAsync(request);
            return Created("api/agendamentos",  agendamento);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [HttpPut("concluir/{id}")]
    public async Task<ActionResult<AgendamentoResponseDto>> ConcluirAsync(int id)
    {
        try
        {
            var agendamento = await _agendamentoService.ConcluirAsync(id);
            return Ok(agendamento);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    
    [HttpPut("cancelar/{id}")]
    public async Task<ActionResult<AgendamentoResponseDto>> CancelarAsync(int id)
    {
        try
        {
            var agendamento = await _agendamentoService.CancelarAsync(id);
            return Ok(agendamento);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    
    [HttpGet("{id}")]
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
    public async Task<ActionResult<AgendamentoResponseDto>> AtualizarAsync(int id, AgendamentoRequestDto request)
    {
        try
        {
            var agendamento = await _agendamentoService.AtualizarAsync(id, request);
            return Ok(agendamento);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}