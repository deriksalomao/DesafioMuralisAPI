using Muralis.Desafio.Api.Dtos;

namespace Muralis.Desafio.Api.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<LeituraClienteDto>> ListaClientes();

        Task<LeituraClienteDto?> ObtemClientePorId(int id);

        Task<IEnumerable<LeituraClienteDto>> BuscaClientePorNome(string nome);

        Task<bool> ClienteJaExiste(string nome);

        Task<LeituraClienteDto> CriaCliente(CriaClienteDto clienteDto);

        Task<bool> AtualizaCliente(int id, AtualizaClienteDto clienteDto);

        Task<bool> RemoveCliente(int id);
    }
}