using Microsoft.AspNetCore.Mvc;
using sistema_acessidoc.Models.Arquivos.Formulario;
using sistema_acessidoc.Services;

namespace sistema_acessidoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly FileUploadService _fileUploadService;

        public HomeController(FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }
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
                var uploadResult = _fileUploadService.UploadFile(viewModel.UploadedProva);
                if (!uploadResult.Success)
                {
                    ModelState.AddModelError("UploadedProva", uploadResult.Message);
                }
                else
                {

                }
            }

            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            ViewBag.SuccessMessage = "Arquivo carregado com sucesso.";
            return View("Index", viewModel);
        }

    }
}

