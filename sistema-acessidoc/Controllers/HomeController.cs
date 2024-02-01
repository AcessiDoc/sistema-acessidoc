using Microsoft.AspNetCore.Mvc;
using sistema_acessidoc.Models.Arquivos.Formulario;
using System.Reflection;

namespace sistema_acessidoc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessarFormulario(IFormFile fileUploader, int tamanhoFonte)
        {
            if (ModelState.IsValid)
            {
                if (fileUploader != null)
                {
                }
            }
            return View("Index");
        }
    }
}