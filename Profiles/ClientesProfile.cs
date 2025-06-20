using AutoMapper;
using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Models;

namespace Muralis.Desafio.Api.Profiles
{
    public class ClientesProfile : Profile
    {
        public ClientesProfile()
        {
            // Mapeamentos para criação e atualização (entrada de dados)
            CreateMap<CriaClienteDto, Cliente>();
            CreateMap<AtualizaClienteDto, Cliente>();

            // Mapeamentos para DTOs aninhados
            CreateMap<EnderecoDto, Endereco>();
            CreateMap<ContatoDto, Contato>();

            // Mapeamento para leitura (saída de dados)
            CreateMap<Cliente, LeituraClienteDto>();
            // Mapeamentos para DTOs de leitura aninhados
            CreateMap<Endereco, EnderecoRetornoDto>();
            CreateMap<Contato, ContatoDto>();
        }
    }
}