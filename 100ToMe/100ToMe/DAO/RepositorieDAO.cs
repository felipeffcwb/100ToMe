using _100ToMe.Data;
using _100ToMe.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _100ToMe.DAO
{
    public class RepositorieDAO
    {
        private readonly ApplicationDbContext _context;

        public RepositorieDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        internal bool AdicionarRepositories(Repositorie repositorie)
        {
            try
            {
                _context.Add(repositorie);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
