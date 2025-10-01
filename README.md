# 🥖 PadariaTech: CRUD com C# e Entity Framework Core

## 📝 Tema Escolhido e Funcionalidade da Aplicação

O projeto **PadariaTech** simula o sistema de gerenciamento de estoque e catálogo de uma padaria.

O objetivo principal é demonstrar a implementação completa das quatro operações básicas de persistência de dados (**CRUD**) em C#, utilizando o **Entity Framework Core (EF Core)** com o paradigma **Code First**.

A aplicação gerencia as seguintes entidades:

* **`Produto`**: Representa um item vendido (ex: Pão Francês, Bolo de Cenoura), contendo nome, preço, quantidade em estoque e uma referência à sua categoria.
* **`Categoria`**: Categoriza os produtos (ex: Pães, Bolos, Doces).

## 🚀 Como Configurar e Rodar o Projeto

### 📋 Pré-requisitos

1.  **SDK do .NET Core (versão 6.0 ou superior)** instalado.
2.  **Git** instalado.

### ⚙️ Configuração e Instalação

1.  **Clone o Repositório:**
    ```bash
    git clone [Link do seu GitHub]
    cd NomeDoSeuProjeto
    ```

2.  **Instalar Pacotes NuGet:**
    Certifique-se de ter os pacotes necessários instalados. Se estiver usando o Visual Studio, eles podem ser instalados via Gerenciador de Pacotes NuGet; se estiver usando a CLI:
    ```bash
    dotnet restore
    ```

3.  **Criar e Aplicar Migrações:**
    Como o projeto usa o EF Core Code First, é necessário criar uma migração inicial para gerar o banco de dados (SQLite, neste caso).
    * **Criar Migração:**
        ```bash
        dotnet ef migrations add InitialCreate
        ```
    * **Aplicar ao Banco de Dados:**
        ```bash
        dotnet ef database update
        ```
    Isso criará o arquivo `PadariaDB.db` na pasta do projeto, com as tabelas `Produtos` e `Categorias` já configuradas (incluindo o *seeding* inicial das categorias).

### ▶️ Como Executar a Aplicação

Execute a aplicação a partir da linha de comando:

```bash
dotnet run
```


### Integrantes

- Vitor Shimizu 550390
- Fabrizio Maia 551869
- Victor Asfur 551684
- André Soler 98827
