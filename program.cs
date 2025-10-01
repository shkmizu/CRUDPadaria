using Microsoft.EntityFrameworkCore;
using PadariaCRUD.Data;
using PadariaCRUD.Models;

// Classe com a lógica de negócio e as operações CRUD (manter inalterada)
public class PadariaService
{
    public void AdicionarProduto(Produto novoProduto)
    {
        using (var context = new PadariaContext())
        {
            context.Produtos.Add(novoProduto);
            context.SaveChanges();
            Console.WriteLine($"\n✅ SUCESSO! Produto '{novoProduto.Nome}' (ID: {novoProduto.Id}) adicionado.");
        }
    }

    public void ListarTodosProdutos()
    {
        using (var context = new PadariaContext())
        {
            var produtos = context.Produtos.Include(p => p.Categoria).ToList();
            
            Console.WriteLine("\n--- LISTAGEM DE PRODUTOS ---");
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto em estoque.");
                return;
            }

            foreach (var p in produtos)
            {
                Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome.PadRight(20)} | Preço: {p.Preco:C} | Estoque: {p.QuantidadeEmEstoque.ToString().PadLeft(3)} | Categoria: {p.Categoria.Nome}");
            }
            Console.WriteLine("---------------------------\n");
        }
    }
    
    // Método para simplificar a atualização (manter inalterada)
    public void AtualizarPrecoProduto(int id, decimal novoPreco)
    {
        using (var context = new PadariaContext())
        {
            var produto = context.Produtos.Find(id);

            if (produto != null)
            {
                produto.Preco = novoPreco;
                context.Produtos.Update(produto);
                context.SaveChanges();
                Console.WriteLine($"\n✅ SUCESSO! Preço do produto ID {id} atualizado para {novoPreco:C}.");
            }
            else
            {
                Console.WriteLine($"\n❌ ERRO! Produto ID {id} não encontrado para atualização.");
            }
        }
    }

    public void DeletarProduto(int id)
    {
        using (var context = new PadariaContext())
        {
            var produto = context.Produtos.Find(id);

            if (produto != null)
            {
                context.Produtos.Remove(produto);
                context.SaveChanges();
                Console.WriteLine($"\n✅ SUCESSO! Produto ID {id} removido do estoque.");
            }
            else
            {
                Console.WriteLine($"\n❌ ERRO! Produto ID {id} não encontrado para deleção.");
            }
        }
    }
    
    // Novo método para listar as categorias disponíveis (necessário para o CREATE)
    public List<Categoria> ListarCategorias()
    {
        using (var context = new PadariaContext())
        {
            return context.Categorias.ToList();
        }
    }
}

// Lógica principal do Menu
class Program
{
    private static readonly PadariaService service = new PadariaService();

    static void Main(string[] args)
    {
        Console.WriteLine("Iniciando PadariaCRUD...");
        
        // Garante que o banco de dados esteja criado e atualizado
        using (var context = new PadariaContext())
        {
             // Esta linha é ideal, mas se não for a primeira vez, pode ser lenta.
             // Para desenvolvimento, é melhor rodar 'dotnet ef database update' na CLI
             // context.Database.Migrate(); 
        }

        Console.WriteLine("Bem-vindo ao Gerenciamento de Estoque da Padaria!");
        
        bool running = true;
        while (running)
        {
            ExibirMenuPrincipal();
            string? choice = Console.ReadLine();

            switch (choice?.ToLower())
            {
                case "1":
                case "c":
                    HandleAdicionarProduto();
                    break;
                case "2":
                case "r":
                    service.ListarTodosProdutos();
                    break;
                case "3":
                case "u":
                    HandleAtualizarProduto();
                    break; // Adicionado apenas para completar a interface
                case "4":
                case "d":
                    HandleDeletarProduto();
                    break;
                case "5":
                case "s":
                    running = false;
                    break;
                default:
                    Console.WriteLine("\nOpção inválida. Tente novamente.");
                    break;
            }
            if (running)
            {
                 Console.WriteLine("\nPressione qualquer tecla para continuar...");
                 Console.ReadKey();
            }
        }

        Console.WriteLine("\nObrigado por usar o PadariaCRUD! Encerrando...");
    }

    static void ExibirMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("            MENU PRINCIPAL - PADARIA");
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("1. [C]riar Produto");
        Console.WriteLine("2. [R]ead - Visualizar Todos os Produtos");
        Console.WriteLine("3. [U]pdate - Atualizar Preço (Exemplo)");
        Console.WriteLine("4. [D]elete - Remover Produto");
        Console.WriteLine("5. [S]air");
        Console.Write("Escolha uma opção (1-5): ");
    }
    
    // --- Handlers de Input do Usuário ---

    static void HandleAdicionarProduto()
    {
        Console.Clear();
        Console.WriteLine("--- ADICIONAR NOVO PRODUTO ---");
        
        var novoProduto = new Produto();

        Console.Write("Nome do Produto: ");
        novoProduto.Nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Preço (ex: 5.50): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal preco))
        {
            novoProduto.Preco = preco;
        }
        else
        {
            Console.WriteLine("Preço inválido. Usando 0.00.");
            novoProduto.Preco = 0.00m;
        }

        Console.Write("Quantidade em Estoque: ");
        if (int.TryParse(Console.ReadLine(), out int estoque))
        {
            novoProduto.QuantidadeEmEstoque = estoque;
        }
        else
        {
            Console.WriteLine("Estoque inválido. Usando 0.");
            novoProduto.QuantidadeEmEstoque = 0;
        }

        // Seleção de Categoria
        var categorias = service.ListarCategorias();
        Console.WriteLine("\nCategorias disponíveis:");
        foreach (var cat in categorias)
        {
            Console.WriteLine($"  {cat.Id}: {cat.Nome}");
        }
        
        Console.Write("Digite o ID da Categoria: ");
        if (int.TryParse(Console.ReadLine(), out int categoriaId) && categorias.Any(c => c.Id == categoriaId))
        {
            novoProduto.CategoriaId = categoriaId;
            service.AdicionarProduto(novoProduto);
        }
        else
        {
            Console.WriteLine("\n❌ ERRO! ID de Categoria inválido ou não encontrado. Produto não adicionado.");
        }
    }

    static void HandleDeletarProduto()
    {
        Console.Clear();
        Console.WriteLine("--- REMOVER PRODUTO ---");
        service.ListarTodosProdutos(); // Ajuda o usuário a ver os IDs
        
        Console.Write("Digite o ID do Produto que deseja remover: ");
        if (int.TryParse(Console.ReadLine(), out int idParaDeletar))
        {
            service.DeletarProduto(idParaDeletar);
        }
        else
        {
            Console.WriteLine("\n❌ ERRO! ID inválido.");
        }
    }
    
    // Handler para Update (apenas para completar o menu)
    static void HandleAtualizarProduto()
    {
        Console.Clear();
        Console.WriteLine("--- ATUALIZAR PREÇO DE PRODUTO ---");
        service.ListarTodosProdutos(); 
        
        Console.Write("Digite o ID do Produto para atualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int idParaAtualizar))
        {
            Console.WriteLine("\n❌ ERRO! ID inválido.");
            return;
        }
        
        Console.Write("Digite o NOVO PREÇO: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal novoPreco))
        {
            service.AtualizarPrecoProduto(idParaAtualizar, novoPreco);
        }
        else
        {
            Console.WriteLine("\n❌ ERRO! Preço inválido.");
        }
    }
}