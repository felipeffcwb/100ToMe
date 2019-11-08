using _100ToMe.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _100ToMe.Models
{
    [Table("Repositories")]
    public class Repositorie
    {
        [Key]
        public int RepoId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string QuantFiles { get; set; }
        public string Link { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataRegister { get; set; } = DateTimeBR.DataHoraAtual();
        public DateTime DataLastChange { get; set; } = DateTimeBR.DataHoraAtual();
    }
}
