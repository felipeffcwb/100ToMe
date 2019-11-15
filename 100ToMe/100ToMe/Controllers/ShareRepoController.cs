using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _100ToMe.DAO;
using _100ToMe.Helpers;
using _100ToMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace _100ToMe.Controllers
{
    public class ShareRepoController : Controller
    {
        private readonly RepositorieDAO _repositorieDAO;

        public ShareRepoController(RepositorieDAO repositorioDAO)
        {
            _repositorieDAO = repositorioDAO;
        }

        public async Task<IActionResult> IndexAsync(string repoLink, string email, string nome)
        {
            if (string.IsNullOrEmpty(repoLink))
            {
                return RedirectToAction("Index", "Home");
            }

            Repositorie repositorie = new Repositorie();
            repositorie = _repositorieDAO.BuscarRepoPorFileId(repoLink);
            if (repositorie != null && repositorie.QuantFiles != 0)
            {
                ViewBag.NameUser = repositorie.Email;
            } else
            {
                ViewBag.LinkNull = "Este link expirou, foi excluido ou não possui arquivos... =(";
            }
            
            if (!string.IsNullOrEmpty(email))
            {
                List<Files> files = new List<Files>();
                files = _repositorieDAO.BuscarFilesPorRepo(repoLink);
                await EmailFiles.EnviarAsync(files, email, nome);
                ViewBag.SuccessShare = "Ok, olhe sua caixa de emails !";
                return View();
            }
            ViewBag.repoLink = repoLink;
            return View();
        }
    }
}