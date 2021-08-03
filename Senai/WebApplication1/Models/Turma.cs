using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Turma
    {
        [Key]
        public int tID { get; set; }
        public string tNome { get; set; }
        public byte tAtivo { get; set; }

        public ICollection<Alunos> TurmaAlunos { get; set; }
    }
}
