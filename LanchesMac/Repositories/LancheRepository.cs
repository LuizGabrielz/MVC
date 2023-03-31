using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Lanche> Lanches  => _context.Lanches.Include(c=> c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.
                                    Where(l=> l.IsLanchePreferido)
                                    .Include(c => c.Categoria);

        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
} 

//A classe "LancheRepository" implementa a interface "ILancheRepository", que define os métodos que devem ser implementados para acessar os dados dos lanches.
//O construtor da classe recebe um objeto "AppDbContext", que é a classe que representa o contexto do banco de dados usado pelo aplicativo. Esse objeto é armazenado em uma variável privada "_context", que é usada pelos métodos do repositório para acessar e manipular os dados dos lanches.
//O primeiro método implementado na classe é o "Lanches", que retorna uma lista de todos os lanches do banco de dados, incluindo suas categorias relacionadas. Isso é feito usando a propriedade "Include" do Entity Framework Core, que carrega as entidades relacionadas (categorias) em uma única consulta ao banco de dados para melhorar o desempenho.
//O segundo método é o "LanchesPreferidos", que retorna uma lista de lanches marcados como "lanche preferido" no banco de dados, incluindo suas categorias relacionadas. Isso é feito usando o método "Where" para filtrar os lanches preferidos e a propriedade "Include" para carregar as categorias relacionadas.
//O terceiro e último método é o "GetLancheById", que retorna um único lanche do banco de dados com base no seu ID. Isso é feito usando o método "FirstOrDefault" para buscar o primeiro lanche com o ID fornecido.
//Em resumo, essa classe é responsável por acessar e manipular os dados da entidade "Lanche" no banco de dados por meio de consultas e operações CRUD (Create, Read, Update, Delete) usando o Entity Framework Core.