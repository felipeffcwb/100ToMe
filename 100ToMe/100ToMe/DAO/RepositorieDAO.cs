using _100ToMe.Data;
using _100ToMe.Helpers;
using _100ToMe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        //Adiciona novos repositórios
        internal bool AdicionarRepositories(Repositorie repositorie)
        {
            try
            {
                if (VerificaSeJaExisteRepoPeloNome(repositorie.UserId, repositorie.Name))
                {
                    _context.Add(repositorie);
                    _context.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        internal bool ExcluirRepo(int repoId)
        {
            try
            {
                Repositorie repositorie = BuscarRepoPorId(repoId);
                repositorie.DataLastChange = DateTimeBR.DataHoraAtual();
                repositorie.Status = false;
                _context.Entry(repositorie).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private Repositorie BuscarRepoPorId(int repoid)
        {
            try
            {

                Repositorie repositorie = new Repositorie();
                repositorie = _context.repositories.FirstOrDefault(x => x.RepoId.Equals(repoid) && x.Status);
                return repositorie;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Busca repositórios criados pelo userId
        internal List<Repositorie> BuscarRepoDeUser(string userId)
        {
            try
            {
                return _context.repositories.Where(x => x.UserId.Equals(userId) && x.Status).ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        internal bool VerificaSeJaExisteRepoPeloNome(string userId, string nome)
        {
            try
            {
                Repositorie repositorie = _context.repositories.FirstOrDefault(x => x.UserId.Equals(userId) && x.Name.Equals(nome) && x.Status);
                if (repositorie == null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal bool AddFile(Files files)
        {
            try
            {
                _context.files.Add(files);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        internal List<Files> BuscarFilesPorRepo(string fileId)
        {
            try
            {
                return _context.files.Where(x => x.FileId.Equals(fileId) && x.Status).ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
