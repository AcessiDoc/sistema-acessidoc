using Microsoft.AspNetCore.Mvc;
using sistema_acessidoc.Models.Arquivos.Formulario;

namespace sistema_acessidoc.Controllers
{
    public class ProvaController : Controller
    {
        [HttpPost]
        public IActionResult UploadLivro(Arquivo arquivo)
        {
            if (ModelState.IsValid)
            {
            }

            return View(arquivo);
        }
    }
}
