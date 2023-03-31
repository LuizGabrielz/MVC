using LanchesMac.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        } 
        public string CarrinhoCompraId { get; set; } // Propiedade do tipo String
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set;} // Definir uma lista que é carrinho compra item

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
       
            //atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId); 
        
            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);
            
            if(carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s. CarrinhoCompraId == CarrinhoCompraId);

                var quantidadeLocal = 0;

                if (carrinhoCompraItem != null)
                {
                    if (carrinhoCompraItem.Quantidade > 1)
                    {
                        carrinhoCompraItem.Quantidade--;
                        quantidadeLocal = carrinhoCompraItem.Quantidade;
                    }
                    else
                    {
                        _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                    }
             }
             _context.SaveChanges();
             return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems ??
                    (CarrinhoCompraItems =
                    _context.CarrinhoCompraItens
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Include(s => s.Lanche)
                    .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                                .Where(carrinho => CarrinhoCompraId == CarrinhoCompraId);

               _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
               _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;
        }

    }
}
//Método Adicionar Ao Carrinho é um método que representa um carrinho de compras em um sistema de comécio eletronico.Ele é responsável por adicionar um novo item ao carrinho de comrpas ou aumentar a quantidade de um item ja existente no carrinho

//O método recebe um objeto "Lanche" como parametro, que representa o item que deve ser adicionado ao carrinho. Ele verifica se ja existe um item no carrinho com o mesmo ID do item que está sendo adicionado. Para isso, usa o método "SingleOrDefault" do LINQ para encontrar o item correspondente no conjunto de itens do carrinho.

// Se não houver nenhum item correspondente, o método simplismente incrementa a quantidade desse item em 1, usando o operador de incremento++

// Por fim, o método salva as alterações no banco de dados usando o método "SaveChanges" do contexto do banco de dados.



// Método chamado RemoverDoCarrinho: que recebe um objeto Lanche como parametro de entrada e retorna um valor inteiro representando a quantidade do item removido do carrinho de compras. Aqui está o que o código faz:

//1. Ele usa o Enity Framework para encontrar um item especifico no carrinho de compras (representado pela variável "carrinhoCompraItem") com base em suas propriedades "LancheId" e "CarrinhoCompraId".

//2. Se o item for encontrado no carrinho de compras, o código verifica se a quantidade do item é maior que um, o item é removido do carrinho de compras usando a instrução "_context.CarrinhoCompraItens.Remove(CarrinhoCompraItem)".

//3. Por fim, o código salva as alterações no carrinho de compras usando a instrução "_context.SaveChanges()" e retorna a variável "quantidadeLocal", que representa a quantidade do item que foi removido do carrinho de compras.

// Em resumo, este código parece fazer parte de um aplicativo maior que gerencia um carrinho de compras, e este método é responsável por remover um item do carrinho.

// Método que retorna uma lista de objetos "CarrinhoCompraItem", que representam itens em um carrinho de compras.
// O método verifica se a propriedade "CarrinhoCompraItems" é nula. Se for nula, o método recupera os itens do banco de dados usando o objeto "_context", que provavelmente é uma instancia de um DbContext.
// O método ".Where(c => c.CarrinhoCompraId == CarrinhoCompraId)" filtra os itens pela propriedade "CarrinhoCompraId", que provavelmente é o identificador do carrinho de compras atual. O método ".Includes(s => s.Lanche)" carrega antecipadamente os objetos "Lanche" relacionados para cada "CarrinhoCompraItem", o que pode melhorar o desempenho reduzindo o número de consultas necessárias ao banco de dados.
// Por fim, o método ".ToList()" retorna a lista de carrinho de compras recuperados do banco de dados.

// Método que limpa todos os itens do carrinho de compras atual
// Primeiro, o método recupera todos os itens do carrinho de compras atual do banco de dados usando o objeto "_context" e o método ".Where(carrinho => CarrinhoCompraId == CarrinhoCompraId"). Este método filtra os itens de carrinho de compras pelo ID do carrinho de compras atual.
// Em seguida, o método removeos itens do carrinho de compras usando o método "_context.CarrinhoCompraItens.RemoveRange(carrinhoItens)", que remove todos os itens passados como parametro em "carrinhoItens".
// Por fim, o método "_context.SaveChanges()" salva as alterações no banco de dados. Depois disso, o carrinho de compras estará vazio.

// Método que calcula o valor total dos itens do carrinho de compras atual, multiplicando o preço de cada item pela quantidade e somando todos os valores
// O método começa recuperando todos os itens do carrinho de compras atual do banco de dados usando o objeto "_context" e o método ".Where(c => c.CarrinhoCompraId == CarrinhoCompraId)". Este método filtra os itens de carrinho de compras pelo ID do carrinho de compras atual.
// Em seguida, o método utiliza o método ".Select(c => c.Lanche.Preco * c.Quantidade)" para projetar um novo conjunto de valores multiplicando o preço de cada item pela quantidade. Este conjunto de valores representa o valor total de cada item.
// Por fim, o método utiliza o método ".Sum()" para somar todos os valores e obter o valor total dos itens do cariinho de compras. O valor total é retornado como um decimal.
