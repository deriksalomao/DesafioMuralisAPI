using Microsoft.EntityFrameworkCore;
using Muralis.Desafio.Api.Models;

namespace Muralis.Desafio.Api.Data
{
    /// <summary>
    /// Contexto do banco de dados para a aplicação, gerenciado pelo Entity Framework Core.
    /// </summary>
    public class ApiDbContext : DbContext
    {
        /// <summary>
        /// Inicializa uma nova instância do contexto do banco de dados.
        /// </summary>
        /// <param name="options">As opções a serem usadas pelo DbContext.</param>
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet para a entidade Cliente. Representa a tabela de clientes no banco de dados.
        /// </summary>
        public DbSet<Cliente> Clientes { get; set; }

        /// <summary>
        /// DbSet para a entidade Endereco. Representa a tabela de endereços no banco de dados.
        /// </summary>
        public DbSet<Endereco> Enderecos { get; set; }

        /// <summary>
        /// DbSet para a entidade Contato. Representa a tabela de contatos no banco de dados.
        /// </summary>
        public DbSet<Contato> Contatos { get; set; }
    }
}