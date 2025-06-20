namespace Muralis.Desafio.Api.Dtos
{
    public class EnderecoRetornoDto
    {
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
        public string Numero { get; set; }

        /// <summary>
        /// Complemento do endereço (apartamento, bloco, etc.). Pode ser nulo.
        /// </summary>
        public string Complemento { get; set; }
    }
}
