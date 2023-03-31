using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context; // Variável privada, que é usada pelos métodos do repositório para acessar e manipular os dados das categorias
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
/*Esse é outro exemplo de implementação de um repositório em um aplicativo web usando o framework .NET Core e o Entity Framework Core como ORM.

A classe "CategoriaRepository" implementa a interface "ICategoriaRepository", que define os métodos que devem ser implementados para acessar os dados das categorias.

O construtor da classe recebe um objeto "AppDbContext", que é a classe que representa o contexto do banco de dados usado pelo aplicativo. Esse objeto é armazenado em uma variável privada "_context", que é usada pelos métodos do repositório para acessar e manipular os dados das categorias.

O único método implementado na classe é o "Categorias", que retorna uma lista de todas as categorias do banco de dados. Isso é feito simplesmente retornando a propriedade "Categorias" do objeto "AppDbContext", que representa a tabela "Categoria" no banco de dados.

Em resumo, essa classe é responsável por acessar e manipular os dados da entidade "Categoria" no banco de dados por meio de consultas e operações CRUD (Create, Read, Update, Delete) usando o Entity Framework Core.
*/
