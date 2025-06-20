using Muralis.Desafio.Api.Dtos;

namespace Muralis.Desafio.Api.Services.Interfaces
{
    public interface IViaCepService
    {
        Task<RespostaViaCepDto?> ObtemEnderecoPorCep(string cep);
    }
}