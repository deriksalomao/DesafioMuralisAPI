using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IEnumerable<LeituraClienteDto>> ListaClientes()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .ToListAsync();
            return _mapper.Map<IEnumerable<LeituraClienteDto>>(clientes);
        }

        /// <inheritdoc />
        public async Task<LeituraClienteDto?> ObtemClientePorId(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<LeituraClienteDto>(cliente);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<LeituraClienteDto>> BuscaClientePorNome(string name)
        {
            var clientes = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .Where(c => c.Nome.Contains(name))
                .ToListAsync();
            return _mapper.Map<IEnumerable<LeituraClienteDto>>(clientes);
        }

        /// <inheritdoc />
        public async Task<LeituraClienteDto> CriaCliente(CriaClienteDto clienteDto)
        {
            if (!ValidarCep(clienteDto.Endereco.Cep))
                throw new InvalidOperationException("O CEP informado é inválido.");

            var addressFromViaCep = await _viaCepService.ObtemEnderecoPorCep(clienteDto.Endereco.Cep);
            if (addressFromViaCep == null || string.IsNullOrEmpty(addressFromViaCep.Logradouro))
            {
                throw new InvalidOperationException($"CEP '{clienteDto.Endereco.Cep}' não encontrado");
            }

            var cliente = _mapper.Map<Cliente>(clienteDto);
            cliente.DataCadastro = DateTime.UtcNow.ToString("o");

            cliente.Endereco.Logradouro = addressFromViaCep.Logradouro;
            cliente.Endereco.Cidade = addressFromViaCep.Localidade;

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return _mapper.Map<LeituraClienteDto>(cliente);
        }

        /// <inheritdoc />
        public async Task<bool> AtualizaCliente(int id, AtualizaClienteDto clienteDto)
        {
            if (!ValidarCep(clienteDto.Endereco.Cep))
                throw new InvalidOperationException("O CEP informado é inválido.");

            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return false;
            }
            var addressFromViaCep = await _viaCepService.ObtemEnderecoPorCep(clienteDto.Endereco.Cep);
            if (addressFromViaCep == null || string.IsNullOrEmpty(addressFromViaCep.Logradouro))
            {
                throw new InvalidOperationException($"CEP '{clienteDto.Endereco.Cep}' não encontrado ou inválido.");
            }

            _mapper.Map(clienteDto, cliente);
            AtualizaContatos(clienteDto.Contatos, cliente.Contatos);
            cliente.Endereco.Logradouro = addressFromViaCep.Logradouro;
            cliente.Endereco.Cidade = addressFromViaCep.Localidade;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        private void AtualizaContatos(ICollection<ContatoDto> contatosDto, ICollection<Contato> contatosExistentes)
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

        private static bool ValidarCep(string cep)
        {
            string cepNumerico = Regex.Replace(cep ?? "", @"[^\d]", "");

            return cepNumerico.Length == 8 &&
                   !Regex.IsMatch(cepNumerico, @"^(\d)\1+$") &&
                   cepNumerico != "00000000";
        }
    }
}