using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Services.Interfaces;
using System.Text.Json;

namespace Muralis.Desafio.Api.Services
{
    /// <summary>
    /// Implementação do serviço para consultar a API externa do ViaCep.
    /// </summary>
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Inicializa uma nova instância do serviço ViaCep.
        /// </summary>
        /// <param name="httpClient">O cliente HTTP injetado para realizar as requisições.</param>
        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtém os dados de endereço correspondentes a um CEP.
        /// </summary>
        /// <remarks>
        /// A tag <c>inheritdoc</c> abaixo herda a documentação da interface <see cref="IViaCepService"/>.
        /// </remarks>
        /// <inheritdoc />
        public async Task<ViaCepResponse?> GetAddressByCepAsync(string cep)
        {
            // Altera a URL para http para contornar possíveis problemas de bloqueio de SSL/TLS em ambientes de desenvolvimento.
            var response = await _httpClient.GetAsync($"http://viacep.com.br/ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            // A desserialização é case-insensitive por padrão, então não há problema com as propriedades do DTO.
            var viaCepResponse = JsonSerializer.Deserialize<ViaCepResponse>(content);

            return viaCepResponse;
        }
    }
}