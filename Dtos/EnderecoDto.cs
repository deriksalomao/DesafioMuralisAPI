namespace Muralis.Desafio.Api.Dtos
{
    /// <summary>
    /// DTO para os dados de um endereço.
    /// </summary>
    public class EnderecoDto
    {
        /// <summary>
        /// CEP do endereço.
        /// </summary>
        public string Cep { get; set; }

        /// <summary>
        /// Logradouro (preenchido pelo ViaCep).
        /// </summary>
        public string? Logradouro { get; set; }

        /// <summary>
        /// Cidade (preenchido pelo ViaCep).
        /// </summary>
        public string? Cidade { get; set; }

        /// <summary>
        /// Número do imóvel no endereço.
        /// </summary>
        public string? Numero { get; set; }

        /// <summary>
        /// Complemento do endereço (apartamento, bloco, etc.).
        /// </summary>
        public string? Complemento { get; set; }
    }
}