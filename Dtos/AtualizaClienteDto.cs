namespace Muralis.Desafio.Api.Dtos
{
    public class AtualizaClienteDto
    {
        public string Nome { get; set; }

        public EnderecoDto Endereco { get; set; }

        public ICollection<ContatoDto> Contatos { get; set; }
    }
}