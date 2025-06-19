namespace Muralis.Desafio.Api.Exceptions
{
    /// <summary>
    /// Exceção lançada quando um recurso específico (como um Cliente) não é encontrado no banco de dados.
    /// </summary>
    public class RecursoNaoEncontrado  : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceção de recurso não encontrado.
        /// </summary>
        /// <param name="message">A mensagem que descreve o erro.</param>
        public RecursoNaoEncontrado (string message) : base(message)
        {
        }
    }
}