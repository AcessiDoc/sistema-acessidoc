using Microsoft.AspNetCore.Mvc;
using sistema_acessidoc.Models.Arquivos.Formulario;

namespace sistema_acessidoc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new LivroProvaViewModel
            {
                Prova = new Arquivo()
            };

            return View(viewModel);
        }

        public IActionResult AcessoRapido()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessarFormulario(LivroProvaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Prova != null)
                {
                    return RedirectToAction("UploadLivro", "Prova", viewModel.Prova);
                }
            }
            return View("Index", viewModel);
        }
    }
}