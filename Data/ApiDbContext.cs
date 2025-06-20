using Microsoft.EntityFrameworkCore;
using Muralis.Desafio.Api.Models;

namespace Muralis.Desafio.Api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Contato> Contatos { get; set; }
    }
}