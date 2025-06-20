using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Muralis.Desafio.Api.Data;
using Muralis.Desafio.Api.Dtos;
using Muralis.Desafio.Api.Models;
using Muralis.Desafio.Api.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Muralis.Desafio.Api.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IViaCepService _viaCepService;

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
        public async Task<IEnumerable<LeituraClienteDto>> BuscaClientePorNome(string nome)
        {
            var clientes = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .Where(c => EF.Functions.ILike(c.Nome, $"%{nome}%"))
                .ToListAsync();
            return _mapper.Map<IEnumerable<LeituraClienteDto>>(clientes);
        }

        /// <inheritdoc />
        public async Task<bool> ClienteJaExiste(string nome)
        {
            return await _context.Clientes.AnyAsync(c => c.Nome.ToLower() == nome.ToLower());
        }

        /// <inheritdoc />
        public async Task<LeituraClienteDto> CriaCliente(CriaClienteDto clienteDto)
        {
            if (await ClienteJaExiste(clienteDto.Nome))
            {
                throw new InvalidOperationException($"Já existe um cliente com o nome '{clienteDto.Nome}'.");
            }

            if (!ValidarCep(clienteDto.Endereco.Cep))
                throw new InvalidOperationException("O CEP informado é inválido.");

            var addressFromViaCep = await _viaCepService.ObtemEnderecoPorCep(clienteDto.Endereco.Cep);
            if (addressFromViaCep == null || addressFromViaCep.Erro)
            {
                throw new InvalidOperationException($"CEP '{clienteDto.Endereco.Cep}' não encontrado ou inválido.");
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
            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return false;
            }

            if (!ValidarCep(clienteDto.Endereco.Cep))
                throw new InvalidOperationException("O CEP informado é inválido.");

            var addressFromViaCep = await _viaCepService.ObtemEnderecoPorCep(clienteDto.Endereco.Cep);
            if (addressFromViaCep == null || addressFromViaCep.Erro)
            {
                throw new InvalidOperationException($"CEP '{clienteDto.Endereco.Cep}' não encontrado ou inválido.");
            }

            _mapper.Map(clienteDto, cliente);

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

        private static bool ValidarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;

            var cepLimpo = Regex.Replace(cep, "[^0-9]", "");
            return cepLimpo.Length == 8 && cepLimpo.Distinct().Count() > 1;
        }
    }
}