namespace Muralis.Desafio.Api.Models
{
    /// <summary>
    /// Representa a entidade Cliente no banco de dados.
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Identificador único do cliente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do cliente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Data e hora do cadastro do cliente, em formato de string.
        /// </summary>
        public string DataCadastro { get; set; }

        /// <summary>
        /// Propriedade de navegação para o endereço do cliente.
        /// </summary>
        public Endereco Endereco { get; set; }

        /// <summary>
        /// Coleção de contatos associados ao cliente.
        /// </summary>
        public ICollection<Contato> Contatos { get; set; } = new List<Contato>();
    }
}