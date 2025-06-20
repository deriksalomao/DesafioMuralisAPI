namespace Muralis.Desafio.Api.Dtos
{
    public class LeituraClienteDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string DataCadastro { get; set; }

        public EnderecoRetornoDto Endereco { get; set; }

        public ICollection<ContatoDto> Contatos { get; set; }
    }
}