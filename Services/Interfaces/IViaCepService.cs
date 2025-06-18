using Muralis.Desafio.Api.Dtos;

namespace Muralis.Desafio.Api.Services.Interfaces
{
    /// <summary>
    /// Define o contrato para o serviço de consulta de CEP.
    /// </summary>
    public interface IViaCepService
    {
        /// <summary>
        /// Obtém os dados de endereço correspondentes a um CEP.
        /// </summary>
        /// <param name="cep">O CEP a ser consultado (deve conter apenas números).</param>
        /// <returns>Um objeto com os dados do endereço ou null se o CEP não for encontrado.</returns>
        Task<ViaCepResponse?> GetAddressByCepAsync(string cep);
    }
}