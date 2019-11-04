using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _100ToMe.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _100ToMe.Controllers
{
    public class PerfilController : Controller
    {
        private UserManager<IdentityUser> _userManager;

        public PerfilController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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
                return View();
            }
            else
            {
                ViewModel VM = new ViewModel();
                VM.StatusMessage = "Por favor, entre em seu email e confirme sua conta para continuar usando sua conta";
                return View(VM);
            }
        }
    }
}