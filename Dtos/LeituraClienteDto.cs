namespace Muralis.Desafio.Api.Dtos
{
    /// <summary>
    /// DTO para a leitura dos dados de um cliente.
    /// </summary>
    public class LeituraClienteDto
    {
        /// <summary>
        /// Identificador único do cliente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Data de cadastro do cliente.
        /// </summary>
        public string DataCadastro { get; set; }

        /// <summary>
        /// Endereço do cliente.
        /// </summary>
        public EnderecoDto Endereco { get; set; }

        /// <summary>
        /// Lista de contatos do cliente.
        /// </summary>
        public ICollection<ContatoDto> Contatos { get; set; }
    }
}