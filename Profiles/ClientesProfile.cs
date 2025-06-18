using AutoMapper;
using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Models;

namespace Muralis.Desafio.Api.Profiles
{
    /// <summary>
    /// Configura os mapeamentos do AutoMapper entre os modelos de domínio e os DTOs.
    /// </summary>
    public class ClientesProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância do perfil de mapeamento de clientes.
        /// </summary>
        public ClientesProfile()
        {
            // Mapeamentos de DTO para Modelo
            CreateMap<ClienteCreateDto, Cliente>();
            CreateMap<UpdateClienteDto, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignora o ID na atualização
            CreateMap<ContatoDto, Contato>();
            CreateMap<EnderecoDto, Endereco>();

            // Mapeamentos de Modelo para DTO
            CreateMap<Cliente, ClienteReadDto>();
            CreateMap<Contato, ContatoDto>();
            CreateMap<Endereco, EnderecoDto>();
        }
    }
}