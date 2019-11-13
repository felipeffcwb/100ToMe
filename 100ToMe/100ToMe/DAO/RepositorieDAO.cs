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

                List<Files> files = new List<Files>();
                files = BuscarFilesPorRepo(repositorie.FileId);
                foreach (Files item in files)
                {
                    item.DataLastChange = DateTimeBR.DataHoraAtual();
                    item.Status = false;
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }
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

        internal Repositorie BuscarRepoPorFileId(string fileId)
        {
            try
            {
                return _context.repositories.FirstOrDefault(x => x.FileId.Equals(fileId));
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void AtualizarQuantFiles(Repositorie repositorie)
        {
            try
            {
                repositorie.DataLastChange = DateTimeBR.DataHoraAtual();
                _context.Entry(repositorie).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal Files BuscarFilePorId(int fileId)
        {
            try
            {
                Files files = new Files();
                files = _context.files.FirstOrDefault(x => x.ArquivoId.Equals(fileId));
                return files;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal bool ExcluirFile(Files files)
        {
            try
            {
                Files files1 = files;
                files1.DataLastChange = DateTimeBR.DataHoraAtual();
                files1.Status = false;
                _context.Entry(files1).State = EntityState.Modified;
                _context.SaveChanges();

                Repositorie repositorie = new Repositorie();
                repositorie = BuscarRepoPorFileId(files1.FileId);
                repositorie.QuantFiles += -1;
                repositorie.DataLastChange = DateTimeBR.DataHoraAtual();
                _context.Entry(repositorie).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
