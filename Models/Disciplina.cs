using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyApiDotNet.Models
{
    public class Disciplina
    {
        public Disciplina() {}

        public Disciplina(int id, string nome, int professorId) 
        {
            this.Id = id;
            this.Nome = nome;
            this.ProfessorId = professorId;
        }
        public int Id { get; set; }

        public string Nome { get; set;}

        //o nome ProfessorId é relacionado ao padrão do EntityFramework
        public int ProfessorId { get; set; }
    
        //o tipo dessa propriedade é Professor porque ela é do 
        //tipo da classe Professor.cs
        public Professor Professor { get; set; }

        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}