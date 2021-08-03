using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class DbSenai
    {
        public static void Senai(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Alunos.Any())
            {
                return;   // DB has been seeded
            }

            var aluno = new Alunos[]
            {
            new Alunos{aNome="Carol",aEmail="carol@gamil.com",cID=2,tID=2,aImagem=null,},
            new Alunos{aNome="Rafael",aEmail="Rafael@gamil.com",cID=2,tID=2,aImagem=null},
            };
            foreach (Alunos s in aluno)
            {
                context.Alunos.Add(s);
            }
            context.SaveChanges();


            var turmas = new Turma[]
            {
                new Turma{tNome="Turma 1",tAtivo=1},
                new Turma{tNome="Turma 2",tAtivo=1},
                new Turma{tNome="Turma 3",tAtivo=1},
                new Turma{tNome="Turma 4",tAtivo=1},
                new Turma{tNome="Turma 5",tAtivo=1},
                new Turma{tNome="Turma 6",tAtivo=0},
            new Turma{tNome="Turma 5",tAtivo=1}         
            };
            foreach (Turma c in turmas)
            {
                context.Turmas.Add(c);
            }
            context.SaveChanges();


            var Categorias = new Categoria[]
            {
            new Categoria{cCategoria="Contabilidade",cAtivo=1},
            new Categoria { cCategoria = "Infra", cAtivo = 1 },
            new Categoria { cCategoria = "TI", cAtivo = 1 },
            new Categoria { cCategoria = "TI", cAtivo = 0 },
            };
            foreach (Categoria e in Categorias)
            {
                context.Categorias.Add(e);
            }
            context.SaveChanges();
        }
    }
    
}
