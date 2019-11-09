using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace _100ToMe.Controllers
{
    public class RepositoriesController : Controller
    {
        public IActionResult Index(string fileId)
        {
            return View();
        }
    }
}