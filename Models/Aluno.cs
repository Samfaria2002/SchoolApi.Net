using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyApiDotNet.Models
{
    public class Aluno
    {

        public Aluno() {}
        public Aluno(int Id, string nome, string sobrenome, int telefone) 
        {
            this.Id = Id;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
   
        }
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public int Telefone { get; set; }

        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}