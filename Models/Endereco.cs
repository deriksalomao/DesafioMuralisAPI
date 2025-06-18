namespace Muralis.Desafio.Api.Models
{
    /// <summary>
    /// Representa a entidade Endereço no banco de dados.
    /// </summary>
    public class Endereco
    {
        /// <summary>
        /// Identificador único do endereço.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// CEP do endereço.
        /// </summary>
        public string Cep { get; set; }

        /// <summary>
        /// Logradouro (rua, avenida, etc.).
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        /// Cidade do endereço.
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        /// Número do imóvel no endereço. Pode ser nulo.
        /// </summary>
        public string? Numero { get; set; }

        /// <summary>
        /// Complemento do endereço (apartamento, bloco, etc.). Pode ser nulo.
        /// </summary>
        public string? Complemento { get; set; }

        /// <summary>
        /// Chave estrangeira para o cliente associado a este endereço.
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// Propriedade de navegação para o cliente.
        /// </summary>
        public Cliente Cliente { get; set; }
    }
}