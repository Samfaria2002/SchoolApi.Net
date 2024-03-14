using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/* Essa classe serve apenas para associar os alunos a suas 
disciplinas*/

namespace UdemyApiDotNet.Models
{
    public class AlunoCurso
    {

        public AlunoCurso()
        {
            
        }
        public AlunoCurso(int alunoId, int cursoId) 
        {
            this.AlunoId = alunoId;
            this.CursoId = cursoId;
   
        }

        public DateTime DataInicio { get; set; } = DateTime.Now;

        public DateTime? DataFim { get; set; }

        public int? Nota { get; set; } = null;

        public int AlunoId { get; set; }

        public Aluno Aluno { get; set; }

        public int CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}