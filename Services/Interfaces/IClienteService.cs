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
        Task<IEnumerable<LeituraClienteDto>> ListaClientes();

        /// <summary>
        /// Busca um cliente específico pelo seu ID de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID do cliente a ser buscado.</param>
        /// <returns>Um DTO com os dados do cliente encontrado ou null se não for encontrado.</returns>
        Task<LeituraClienteDto?> ObtemClientePorId(int id);

        /// <summary>
        /// Pesquisa clientes cujo nome contém o texto fornecido.
        /// </summary>
        /// <param name="name">O texto a ser pesquisado no nome dos clientes.</param>
        /// <returns>Uma coleção de DTOs dos clientes que correspondem ao critério de busca.</returns>
        Task<IEnumerable<LeituraClienteDto>> BuscaClientePorNome(string name);

        /// <summary>
        /// Cria um novo cliente no sistema.
        /// </summary>
        /// <param name="clienteDto">Objeto com os dados para a criação do novo cliente.</param>
        /// <returns>Um DTO com os dados do cliente recém-criado.</returns>
        /// <exception cref="ExcecaoValidacaoCep">Lançada quando o CEP fornecido não é encontrado ou é inválido.</exception>
        Task<LeituraClienteDto> CriaCliente(CriaClienteDto clienteDto);

        /// <summary>
        /// Atualiza os dados de um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="clienteDto">Objeto com os novos dados do cliente.</param>
        /// <returns>Um booleano indicando se a operação foi bem-sucedida.</returns>
        /// <exception cref="ExcecaoRecursoNaoEncontrado ">Lançada se o cliente com o ID especificado não for encontrado.</exception>
        /// <exception cref="ExcecaoValidacaoCep">Lançada quando o CEP fornecido não é encontrado ou é inválido.</exception>
        Task<bool> AtualizaCliente(int id, AtualizaClienteDto clienteDto);

        /// <summary>
        /// Deleta um cliente do sistema.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        /// <returns>Um booleano indicando se a operação foi bem-sucedida.</returns>
        /// <exception cref="ExcecaoRecursoNaoEncontrado ">Lançada se o cliente com o ID especificado não for encontrado.</exception>
        Task<bool> RemoveCliente(int id);
    }
}