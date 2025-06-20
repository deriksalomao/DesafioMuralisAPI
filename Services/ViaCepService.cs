using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Services.Interfaces;
using System.Text.Json;

namespace Muralis.Desafio.Api.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RespostaViaCepDto?> ObtemEnderecoPorCep(string cep)
        {
            var response = await _httpClient.GetAsync($"http://viacep.com.br/ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var RespostaViaCepDto = JsonSerializer.Deserialize<RespostaViaCepDto>(content);

            return RespostaViaCepDto;
        }
    }
}