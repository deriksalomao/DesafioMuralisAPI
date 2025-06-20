using Microsoft.AspNetCore.Mvc;
using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Services.Interfaces;

namespace Muralis.Desafio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LeituraClienteDto>>> ListaClientes()
        {
            var clientesDto = await _clienteService.ListaClientes();
            return Ok(clientesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeituraClienteDto>> ObtemClientePorId(int id)
        {
            var clienteDto = await _clienteService.ObtemClientePorId(id);
            if (clienteDto == null)
            {
                return NotFound($"Cliente com id {id} não encontrado");
            }
            return Ok(clienteDto);
        }

        [HttpGet("pesquisar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<LeituraClienteDto>>> BuscaClientePorNome([FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest("O parâmetro 'nome' para a pesquisa não pode ser vazio.");
            }

            var clientesDto = await _clienteService.BuscaClientePorNome(nome);
            return Ok(clientesDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LeituraClienteDto>> CriaCliente([FromBody] CriaClienteDto clienteDto)
        {
            var novoClienteDto = await _clienteService.CriaCliente(clienteDto);
            return CreatedAtAction(nameof(ObtemClientePorId), new { id = novoClienteDto.Id }, novoClienteDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizaCliente(int id, [FromBody] AtualizaClienteDto clienteDto)
        {
            var resultado = await _clienteService.AtualizaCliente(id, clienteDto);
            if (!resultado)
            {
                return NotFound($"Cliente com id {id} não encontrado");
            }
            return Ok(new { mensagem = "Dados do cliente foram alterados com sucesso" });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCliente(int id)
        {
            var resultado = await _clienteService.RemoveCliente(id);
            if (!resultado)
            {
                return NotFound($"Cliente com id {id} não encontrado");
            }
            return Ok(new { mensagem = "Cliente removido com sucesso" });
        }
    }
}