namespace Muralis.Desafio.Api.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string DataCadastro { get; set; }

        public Endereco Endereco { get; set; }

        public ICollection<Contato> Contatos { get; set; } = new List<Contato>();
    }
}