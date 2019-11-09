using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _100ToMe.DAO;
using _100ToMe.Helpers;
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

            if (!VerificarLogin()) { return Redirect("Identity/Account/Register"); }

            return View(await ListaRepoEAvisaConfirmarContaAsync());
        }

        //Metodo que trás lista de repositorios pelo userId e verifica se deve pedir ao user que confirme a conta.
        private async Task<ViewModel> ListaRepoEAvisaConfirmarContaAsync()
        {
            string userId = _userManager.GetUserId(User);
            if (!await VerificarContaConfirmadaAsync())
            {
                ViewModel VM = new ViewModel();
                VM.StatusMessage = "Por favor, entre em seu email e confirme sua conta para continuar usando sua conta";
                return VM;
            }
            else
            {
                ViewModel VM = new ViewModel();
                VM.repositories = _repositorieDAO.BuscarRepoDeUser(userId);
                return VM;
            }
        }

        //Verifica se o user já fez login.
        private bool VerificarLogin()
        {
            if (!string.IsNullOrEmpty(_userManager.GetUserId(User)))
            {
                return true;
            }
            return false;
        }

        //Verifica se o user já confirmou a conta pelo email.
        private async Task<bool> VerificarContaConfirmadaAsync()
        {
            var userAsync = await _userManager.GetUserAsync(User);
            if (await _userManager.IsEmailConfirmedAsync(userAsync))
            {
                return true;
            }
            return false;
        }

        //Adiciona novos repositórios.
        public bool AddRepo(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Repositorie repositorie = new Repositorie();
                repositorie.Name = name;
                repositorie.UserId = _userManager.GetUserId(User);
                repositorie.Email = _userManager.GetUserName(User);
                repositorie.FileId = _userManager.GetUserId(User) + "_" + DateTimeBR.DataHoraAtual().Ticks;
                if (_repositorieDAO.AdicionarRepositories(repositorie))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ExcluirRepo(int repoId)
        {
            if (_repositorieDAO.ExcluirRepo(repoId))
            {
                return true;
            }
            return false;
        }
    }
}