namespace Muralis.Desafio.Api.Dtos
{
    public class CriaClienteDto
    {
        public string Nome { get; set; }

        public EnderecoDto Endereco { get; set; }

        public ICollection<ContatoDto> Contatos { get; set; } = new List<ContatoDto>();
    }
}