using LanchesMac.Models;

namespace LanchesMac.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; } // Retorna todos os lanches no banco de dados
        IEnumerable<Lanche> LanchesPreferidos { get; } // Retorna uma lista de lanches marcados como "lanche preferido" no banco de dados
        Lanche GetLancheById(int lancheId); // Retorna um único lanche do banco de dados com base no seu ID
    }
}

// Métodos que devem ser implementados por um repositório para acessar os dados da entidade "Lanche" em um aplicativo web usando o framework.NET Core e o Entity Framework Core como Orm.
//Esses métodos definem as operações básicas de acesso e manipulação dos dados da entidade "Lanche" no banco de dados
// Ao definir esses métodos em uma interface separada, é possivel criar vários repositórios que implementam a mesma interface, tornando o código mais modular e fácil de manter. Além disso, essa abordagem permite que o código mais modular e fácil de manter.
// Além disso, essa abordagem permite que ocódigo do aplicatiov depende apenas da interface, em vez de depender diretamente da implementação do repositório, o que facilita a substituição do repositório por um outro com uma implementação diferente, sem precisar alterar o código do aplicativo.

// Esse código é mais um exemplo de uma interface que define os métodos que devem ser implementados por um repositório para acessar os dados da entidade "Lanche"