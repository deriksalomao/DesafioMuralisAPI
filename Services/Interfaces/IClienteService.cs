using Muralis.Desafio.Api.Dtos;

namespace Muralis.Desafio.Api.Services.Interfaces
{
    /// <summary>
    /// Define o contrato para o serviço de gerenciamento de clientes.
    /// </summary>
    public interface IClienteService
    {
        /// <summary>
        /// Obtém todos os clientes cadastrados de forma assíncrona.
        /// </summary>
        /// <returns>Uma coleção de DTOs representando todos os clientes.</returns>
        Task<IEnumerable<ClienteReadDto>> GetAllAsync();

        /// <summary>
        /// Busca um cliente específico pelo seu ID de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID do cliente a ser buscado.</param>
        /// <returns>Um DTO com os dados do cliente encontrado ou null se não for encontrado.</returns>
        Task<ClienteReadDto?> GetByIdAsync(int id);

        /// <summary>
        /// Pesquisa clientes cujo nome contém o texto fornecido.
        /// </summary>
        /// <param name="name">O texto a ser pesquisado no nome dos clientes.</param>
        /// <returns>Uma coleção de DTOs dos clientes que correspondem ao critério de busca.</returns>
        Task<IEnumerable<ClienteReadDto>> SearchByNameAsync(string name);

        /// <summary>
        /// Cria um novo cliente no sistema.
        /// </summary>
        /// <param name="clienteDto">Objeto com os dados para a criação do novo cliente.</param>
        /// <returns>Um DTO com os dados do cliente recém-criado.</returns>
        Task<ClienteReadDto> CreateAsync(ClienteCreateDto clienteDto);

        /// <summary>
        /// Atualiza os dados de um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="clienteDto">Objeto com os novos dados do cliente.</param>
        /// <returns>Um booleano indicando se a operação foi bem-sucedida.</returns>
        Task<bool> UpdateAsync(int id, UpdateClienteDto clienteDto);

        /// <summary>
        /// Deleta um cliente do sistema.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        /// <returns>Um booleano indicando se a operação foi bem-sucedida.</returns>
        Task<bool> DeleteAsync(int id);
    }
}