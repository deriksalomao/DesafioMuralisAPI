using AutoMapper;
using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Models;

namespace Muralis.Desafio.Api.Profiles
{
    public class ClientesProfile : Profile
    {
        public ClientesProfile()
        {
            CreateMap<CriaClienteDto, Cliente>();
            CreateMap<AtualizaClienteDto, Cliente>();
            CreateMap<EnderecoDto, Endereco>();
            CreateMap<ContatoDto, Contato>();
            CreateMap<Cliente, LeituraClienteDto>();
            CreateMap<Endereco, EnderecoRetornoDto>();
            CreateMap<Contato, ContatoDto>();
        }
    }
}