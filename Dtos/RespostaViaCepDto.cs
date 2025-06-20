using System.Text.Json.Serialization;

namespace Muralis.Desafio.Api.Dtos
{
    /// <summary>
    /// Representa a resposta da API externa ViaCep.
    /// </summary>
    public class RespostaViaCepDto
    {
        /// <summary>
        /// Indica se a consulta de CEP resultou em erro.
        /// </summary>
        [JsonPropertyName("erro")]
        public bool Erro { get; set; }

        /// <summary>
        /// CEP retornado pela API.
        /// </summary>
        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        /// <summary>
        /// Logradouro (rua, avenida).
        /// </summary>
        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        /// <summary>
        /// Informações adicionais de endereço.
        /// </summary>
        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        /// <summary>
        /// Cidade.
        /// </summary>
        [JsonPropertyName("localidade")]
        public string Localidade { get; set; }

        /// <summary>
        /// Estado (Unidade Federativa).
        /// </summary>
        [JsonPropertyName("uf")]
        public string Uf { get; set; }

        /// <summary>
        /// Código IBGE do município.
        /// </summary>
        [JsonPropertyName("ibge")]
        public string Ibge { get; set; }
    }
}