using Microsoft.AspNetCore.Mvc;
using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Services.Interfaces;

namespace Muralis.Desafio.Api.Controllers
{
    /// <summary>
    /// API para o gerenciamento de clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        /// <summary>
        /// Inicializa uma nova instância do controlador de clientes.
        /// </summary>
        /// <param name="clienteService">Serviço para operações de cliente.</param>
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obtém uma lista de todos os clientes.
        /// </summary>
        /// <returns>Uma lista de clientes.</returns>
        /// <response code="200">Retorna a lista de clientes.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClienteReadDto>>> GetAllClientes()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        /// <summary>
        /// Obtém um cliente específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente.</param>
        /// <returns>Os dados do cliente solicitado.</returns>
        /// <response code="200">Retorna os dados do cliente.</response>
        /// <response code="404">Se o cliente com o ID especificado não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteReadDto>> GetClienteById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            return Ok(cliente);
        }

        /// <summary>
        /// Pesquisa clientes por nome.
        /// </summary>
        /// <param name="nome">O nome ou parte do nome a ser pesquisado.</param>
        /// <returns>Uma lista de clientes que correspondem ao critério de busca.</returns>
        /// <response code="200">Retorna a lista de clientes encontrados.</response>
        [HttpGet("pesquisar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClienteReadDto>>> PesquisarClientePorNome([FromQuery] string nome)
        {
            var clientes = await _clienteService.SearchByNameAsync(nome);
            return Ok(clientes);
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <remarks>
        /// O CEP fornecido é validado através da API ViaCep.
        /// </remarks>
        /// <param name="clienteDto">Os dados do novo cliente.</param>
        /// <returns>A localização do novo recurso criado.</returns>
        /// <response code="201">Se o cliente foi criado com sucesso. Retorna o cliente criado.</response>
        /// <response code="400">Se os dados fornecidos são inválidos (ex: CEP não encontrado).</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteCreateDto clienteDto)
        {
            var novoCliente = await _clienteService.CreateAsync(clienteDto);
            return CreatedAtAction(nameof(GetClienteById), new { id = novoCliente.Id }, novoCliente);
        }

        /// <summary>
        /// Atualiza os dados de um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="clienteDto">Os novos dados do cliente.</param>
        /// <returns>Nenhum conteúdo.</returns>
        /// <response code="204">Se o cliente foi atualizado com sucesso.</response>
        /// <response code="400">Se os dados fornecidos são inválidos.</response>
        /// <response code="404">Se o cliente com o ID especificado não foi encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarCliente(int id, [FromBody] UpdateClienteDto clienteDto)
        {
            await _clienteService.UpdateAsync(id, clienteDto);
            return NoContent();
        }

        /// <summary>
        /// Deleta um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        /// <returns>Nenhum conteúdo.</returns>
        /// <response code="204">Se o cliente foi deletado com sucesso.</response>
        /// <response code="404">Se o cliente com o ID especificado não foi encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}