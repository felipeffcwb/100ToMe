using _100ToMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _100ToMe.ViewModels
{
    public class ViewModel
    {
        public string StatusMessage { get; set; }
        public List<Repositorie> repositories { get; set; }
    }
}
