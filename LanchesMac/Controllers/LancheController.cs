using Microsoft.AspNetCore.Mvc;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;

namespace LanchesMac.Controllers
{
    //[Route("[controller]")]
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
           ViewData["Titulo"] = "Todos os Lanches";
           ViewData["Data"] = DateTime.Now;

           var lanches = _lancheRepository.Lanches;
           return View(lanches);
        }
    }
}
