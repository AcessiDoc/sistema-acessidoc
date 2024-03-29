﻿using Microsoft.AspNetCore.Mvc;
using sistema_acessidoc.Models.Arquivos.Formulario;

namespace sistema_acessidoc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new LivroProvaViewModel
            {
                Livro = new Arquivo(),
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
                if (viewModel.Livro != null)
                {
                    return RedirectToAction("UploadLivro", "Livro", viewModel.Livro);
                }
                else if (viewModel.Prova != null)
                {
                    return RedirectToAction("UploadLivro", "Prova", viewModel.Prova);
                }
            }
            return View("Index", viewModel);
        }
    }
}