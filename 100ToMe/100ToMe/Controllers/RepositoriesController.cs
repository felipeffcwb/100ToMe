using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using _100ToMe.DAO;
using _100ToMe.Helpers;
using _100ToMe.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _100ToMe.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly RepositorieDAO _repositorieDAO;
        IHostingEnvironment _hostingEnvironment;

        public RepositoriesController(RepositorieDAO repositorieDAO, IHostingEnvironment hostingEnvironment)
        {
            _repositorieDAO = repositorieDAO;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(string fileId)
        {
            HttpContext.Session.SetString("FileId", fileId);
            List<Files> files = new List<Files>();
            files = _repositorieDAO.BuscarFilesPorRepo(fileId);
            if (files.Count > 0)
            {
                return View(files);
            }
            ViewBag.ErrorList = "Você ainda não possui arquivos, suba alguma coisa.";
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(List<IFormFile> arquivos)
        {
            long tamanhoArquivos = arquivos.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();
            var sessionIdFile = HttpContext.Session.GetString("FileId");
            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in arquivos)
            {
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    ViewBag.Erro = "Error: Arquivo(s) não selecionado(s)";
                    return RedirectToAction("Index", "Repositories", new { fileId = sessionIdFile});
                }
                // < define a pasta onde vamos salvar os arquivos >
                string pasta = "Arquivos_Usuario";
                var tickFile = sessionIdFile.Split("_");
                var finalName = Regex.Replace(arquivo.FileName, @"(?<=[^_]*)_.*", String.Empty);
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = finalName + "__" + tickFile[1] + "__" + DateTimeBR.DataHoraAtual().Ticks;
                //verifica qual o tipo de arquivo : jpg, gif, png, pdf ou tmp
                if (arquivo.FileName.Contains(".jpg"))
                    nomeArquivo += ".jpg";
                else if (arquivo.FileName.Contains(".gif"))
                    nomeArquivo += ".gif";
                else if (arquivo.FileName.Contains(".png"))
                    nomeArquivo += ".png";
                else if (arquivo.FileName.Contains(".pdf"))
                    nomeArquivo += ".pdf";
                else
                    nomeArquivo += ".tmp";
                //< obtém o caminho físico da pasta wwwroot >
                string caminho_WebRoot = _hostingEnvironment.WebRootPath;
                // monta o caminho onde vamos salvar o arquivo : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "\\Arquivos\\" + pasta + "\\";
                // incluir a pasta Recebidos e o nome do arquivo enviado : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos\
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "\\Recebidos\\" + nomeArquivo;
                //copia o arquivo para o local de destino original
                string caminhoArquivoDb = caminhoDestinoArquivo + "\\Recebidos\\";
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                var byteSize = arquivo.Length;
                var mbSize = 1048576.0;
                var result = byteSize / mbSize * mbSize;

                Files files = new Files();
                files.FileId = sessionIdFile;
                files.FilePath = caminhoArquivoDb;
                files.Name = nomeArquivo;
                files.Size = result;
                _repositorieDAO.AddFile(files);
            }
            //monta a ViewData que será exibida na view como resultado do envio 
            ViewBag.Success = $"{arquivos.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {tamanhoArquivos} bytes";
            //retorna a viewdata
            return RedirectToAction("Index", "Repositories", new { fileId = sessionIdFile});
        }
    }
}