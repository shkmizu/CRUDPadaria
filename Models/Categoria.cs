namespace PadariaCRUD.Models
{
    public class Categoria
    {
        public int Id { get; set; } // Chave primária
        public string Nome { get; set; } = string.Empty;

        // Relacionamento 1 para N
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}