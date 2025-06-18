namespace Muralis.Desafio.Api.Exceptions
{
    /// <summary>
    /// Exceção lançada quando ocorre um erro na validação ou busca de um CEP.
    /// </summary>
    public class CepValidationException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceção de validação de CEP.
        /// </summary>
        /// <param name="message">A mensagem que descreve o erro.</param>
        public CepValidationException(string message) : base(message)
        {
        }
    }
}