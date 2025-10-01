namespace PadariaCRUD.Models
{
    public class Produto
    {
        public int Id { get; set; } // Chave primária
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }

        // Chave estrangeira
        public int CategoriaId { get; set; }

        // Propriedade de navegação
        public Categoria Categoria { get; set; } = null!;
    }
}