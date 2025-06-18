namespace Muralis.Desafio.Api.Dtos
{
    /// <summary>
    /// DTO para os dados de um contato.
    /// </summary>
    public class ContatoDto
    {
        /// <summary>
        /// Tipo do contato (ex: Email, Telefone).
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// O valor do contato (o email, o número, etc.).
        /// </summary>
        public string Texto { get; set; }
    }
}