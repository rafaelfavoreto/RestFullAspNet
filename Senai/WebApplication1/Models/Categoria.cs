using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Categoria
    {
        [Key]
        public int cID { get; set; }
        public string cCategoria { get; set; }
        public byte cAtivo { get; set; }

        public ICollection<Alunos> CategoriaAlunos { get; set; }
    }
}
