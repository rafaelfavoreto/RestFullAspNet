using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Alunos
    {
        [Key]
        public int aID  { get; set; }
        public string aNome { get; set; }
        public string aEmail { get; set; }
        public int tID { get; set; }
        public int cID { get; set; }
        public string aImagem { get; set; }

        //public Turma Turma { get; set; } 
        //public Categoria Categoria { get; set; }
    }
}
