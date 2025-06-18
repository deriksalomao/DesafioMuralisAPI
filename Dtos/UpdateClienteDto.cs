namespace Muralis.Desafio.Api.Dtos
{
    /// <summary>
    /// DTO para a atualização de um cliente existente.
    /// </summary>
    public class UpdateClienteDto
    {
        /// <summary>
        /// Novo nome do cliente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Novos dados de endereço do cliente.
        /// </summary>
        public EnderecoDto Endereco { get; set; }

        /// <summary>
        /// Nova lista de contatos do cliente. A lista antiga será substituída.
        /// </summary>
        public ICollection<ContatoDto> Contatos { get; set; }
    }
}