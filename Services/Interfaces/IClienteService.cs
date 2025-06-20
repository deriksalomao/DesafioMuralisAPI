using Muralis.Desafio.Api.Dtos;

namespace Muralis.Desafio.Api.Services.Interfaces
{
    /// <summary>
    /// Define o contrato para o serviço de gerenciamento de clientes.
    /// </summary>
    public interface IClienteService
    {
        /// <summary>
        /// Obtém uma lista de todos os clientes.
        /// </summary>
        /// <returns>Uma coleção de DTOs de leitura de clientes.</returns>
        Task<IEnumerable<LeituraClienteDto>> ListaClientes();

        /// <summary>
        /// Obtém um cliente específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente.</param>
        /// <returns>O DTO de leitura do cliente ou nulo se não for encontrado.</returns>
        Task<LeituraClienteDto?> ObtemClientePorId(int id);

        /// <summary>
        /// Busca clientes por nome de forma case-insensitive.
        /// </summary>
        /// <param name="nome">O nome ou parte do nome a ser pesquisado.</param>
        /// <returns>Uma coleção de DTOs de leitura dos clientes encontrados.</returns>
        Task<IEnumerable<LeituraClienteDto>> BuscaClientePorNome(string nome);

        /// <summary>
        /// Verifica se um cliente com o nome especificado já existe (case-insensitive).
        /// </summary>
        /// <param name="nome">O nome do cliente a ser verificado.</param>
        /// <returns>Retorna verdadeiro se o cliente já existe, caso contrário, falso.</returns>
        Task<bool> ClienteJaExiste(string nome);

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="clienteDto">O DTO com os dados para a criação do cliente.</param>
        /// <returns>O DTO de leitura do cliente recém-criado.</returns>
        Task<LeituraClienteDto> CriaCliente(CriaClienteDto clienteDto);

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="clienteDto">O DTO com os dados atualizados.</param>
        /// <returns>Retorna verdadeiro se a atualização for bem-sucedida, caso contrário, falso.</returns>
        Task<bool> AtualizaCliente(int id, AtualizaClienteDto clienteDto);

        /// <summary>
        /// Remove um cliente pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente a ser removido.</param>
        /// <returns>Retorna verdadeiro se a remoção for bem-sucedida, caso contrário, falso.</returns>
        Task<bool> RemoveCliente(int id);
    }
}