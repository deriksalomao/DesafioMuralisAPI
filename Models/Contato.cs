namespace Muralis.Desafio.Api.Models
{
    /// <summary>
    /// Representa a entidade Contato no banco de dados.
    /// </summary>
    public class Contato
    {
        /// <summary>
        /// Identificador único do contato.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tipo do contato (ex: Email, Telefone, Celular).
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// O texto do contato (o endereço de email, o número de telefone, etc.).
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Chave estrangeira para o cliente associado a este contato.
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// Propriedade de navegação para o cliente.
        /// </summary>
        public Cliente Cliente { get; set; }
    }
}