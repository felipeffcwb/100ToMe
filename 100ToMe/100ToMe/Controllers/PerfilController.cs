using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _100ToMe.DAO;
using _100ToMe.Models;
using _100ToMe.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _100ToMe.Controllers
{
    public class PerfilController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private RepositorieDAO _repositorieDAO; 

        public PerfilController(UserManager<IdentityUser> userManager, RepositorieDAO repositorieDAO)
        {
            _userManager = userManager;
            _repositorieDAO = repositorieDAO;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string user = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(_userManager.GetUserId(User)))
            {

                return Redirect("Identity/Account/Register");
            }

            var userEmail = await _userManager.GetUserAsync(User);
            if (await _userManager.IsEmailConfirmedAsync(userEmail))
            {
                ViewModel VM = new ViewModel();
                VM.repositories = _repositorieDAO.BuscarRepoDeUser(user);
                return View(VM);
            }
            else
            {
                ViewModel VM = new ViewModel();
                VM.StatusMessage = "Por favor, entre em seu email e confirme sua conta para continuar usando sua conta";
                VM.repositories = _repositorieDAO.BuscarRepoDeUser(user);
                return View(VM);
            }
        }

        public bool AddRepo(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Repositorie repositorie = new Repositorie();
                repositorie.Name = name;
                repositorie.UserId = _userManager.GetUserId(User);
                repositorie.Email = _userManager.GetUserName(User);
                if (_repositorieDAO.AdicionarRepositories(repositorie))
                {
                    return true;
                }
            }
            return false;
        }
    }
}