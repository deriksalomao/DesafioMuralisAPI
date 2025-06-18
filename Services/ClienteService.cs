using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Muralis.Desafio.Api.Data;
using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Exceptions;
using Muralis.Desafio.Api.Models;
using Muralis.Desafio.Api.Services.Interfaces;

namespace Muralis.Desafio.Api.Services
{
    /// <summary>
    /// Implementação do serviço com a lógica de negócio para gerenciamento de clientes.
    /// </summary>
    public class ClienteService : IClienteService
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IViaCepService _viaCepService;

        /// <summary>
        /// Inicializa uma nova instância do serviço de cliente.
        /// </summary>
        public ClienteService(ApiDbContext context, IMapper mapper, IViaCepService viaCepService)
        {
            _context = context;
            _mapper = mapper;
            _viaCepService = viaCepService;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ClienteReadDto>> GetAllAsync()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ClienteReadDto>>(clientes);
        }

        /// <inheritdoc />
        public async Task<ClienteReadDto?> GetByIdAsync(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                throw new ResourceNotFoundException($"Cliente com ID {id} não foi encontrado.");
            }
            return _mapper.Map<ClienteReadDto>(cliente);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ClienteReadDto>> SearchByNameAsync(string name)
        {
            var clientes = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .Where(c => c.Nome.Contains(name))
                .ToListAsync();
            return _mapper.Map<IEnumerable<ClienteReadDto>>(clientes);
        }

        /// <inheritdoc />
        /// <exception cref="CepValidationException">Lançada quando o CEP fornecido não é encontrado ou é inválido.</exception>
        public async Task<ClienteReadDto> CreateAsync(ClienteCreateDto clienteDto)
        {
            var addressFromViaCep = await _viaCepService.GetAddressByCepAsync(clienteDto.Endereco.Cep);
            if (addressFromViaCep == null || string.IsNullOrEmpty(addressFromViaCep.Logradouro))
            {
                throw new CepValidationException($"CEP '{clienteDto.Endereco.Cep}' não encontrado ou inválido.");
            }

            var cliente = _mapper.Map<Cliente>(clienteDto);
            cliente.DataCadastro = DateTime.UtcNow.ToString("o");

            cliente.Endereco.Logradouro = addressFromViaCep.Logradouro;
            cliente.Endereco.Cidade = addressFromViaCep.Localidade;

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClienteReadDto>(cliente);
        }

        /// <inheritdoc />
        /// <exception cref="ResourceNotFoundException">Lançada se o cliente com o ID especificado não for encontrado.</exception>
        /// <exception cref="CepValidationException">Lançada quando o CEP fornecido não é encontrado ou é inválido.</exception>
        public async Task<bool> UpdateAsync(int id, UpdateClienteDto clienteDto)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                throw new ResourceNotFoundException($"Cliente com ID {id} não foi encontrado para atualização.");
            }

            var addressFromViaCep = await _viaCepService.GetAddressByCepAsync(clienteDto.Endereco.Cep);
            if (addressFromViaCep == null || string.IsNullOrEmpty(addressFromViaCep.Logradouro))
            {
                throw new CepValidationException($"CEP '{clienteDto.Endereco.Cep}' não encontrado ou inválido.");
            }

            _mapper.Map(clienteDto, cliente);
            UpdateContatos(clienteDto.Contatos, cliente.Contatos);
            cliente.Endereco.Logradouro = addressFromViaCep.Logradouro;
            cliente.Endereco.Cidade = addressFromViaCep.Localidade;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        /// <exception cref="ResourceNotFoundException">Lançada se o cliente com o ID especificado não for encontrado.</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                throw new ResourceNotFoundException($"Cliente com ID {id} não foi encontrado para exclusão.");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        private void UpdateContatos(ICollection<ContatoDto> contatosDto, ICollection<Contato> contatosExistentes)
        {
            var contatosParaRemover = contatosExistentes
                .Where(c => !contatosDto.Any(dto => dto.Tipo == c.Tipo && dto.Texto == c.Texto))
                .ToList();
            _context.Contatos.RemoveRange(contatosParaRemover);

            var novosContatos = contatosDto
                .Where(dto => !contatosExistentes.Any(c => c.Tipo == dto.Tipo && c.Texto == dto.Texto))
                .Select(dto => _mapper.Map<Contato>(dto))
                .ToList();

            foreach (var novoContato in novosContatos)
            {
                contatosExistentes.Add(novoContato);
            }
        }
    }
}