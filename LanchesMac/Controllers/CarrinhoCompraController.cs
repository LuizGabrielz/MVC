using Microsoft.AspNetCore.Mvc;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompraController _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository,
            CarrinhoCompraController carrinhoCompra)
            {
                _lancheRepository = lancheRepository;
                _carrinhoCompra = carrinhoCompra;
            }

        public IActionResult Index()
        {
            return View();
        }
    }
}