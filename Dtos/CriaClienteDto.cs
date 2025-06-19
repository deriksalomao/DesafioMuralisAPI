namespace Muralis.Desafio.Api.Dtos
{
    /// <summary>
    /// DTO para a criação de um novo cliente.
    /// </summary>
    public class CriaClienteDto
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Endereço do cliente.
        /// </summary>
        public EnderecoDto Endereco { get; set; }

        /// <summary>
        /// Lista de contatos do cliente.
        /// </summary>
        public ICollection<ContatoDto> Contatos { get; set; } = new List<ContatoDto>();
    }
}