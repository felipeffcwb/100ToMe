using _100ToMe.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _100ToMe.Models
{
    [Table("Files")]
    public class Files
    {
        [Key]
        public int ArquivoId { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public double Size { get; set; }
        public string Name { get; set; }
        public string FileId { get; set; }
        public string ExtensionType { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataRegister { get; set; } = DateTimeBR.DataHoraAtual();
        public DateTime DataLastChange { get; set; } = DateTimeBR.DataHoraAtual();
    }
}
