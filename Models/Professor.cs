using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyApiDotNet.Models
{
    public class Professor
    {

        public Professor() {}

        public Professor(int Id, string Nome, string Sobrenome, int Telefone) {
            this.Id = Id;
            this.Nome = Nome;
            this.Sobrenome = Sobrenome;
            this.Telefone = Telefone;
        }

        public int Id { get; set; }
        
        public string Nome { get; set; }

        public string Sobrenome { get; set; }
        
        public int Telefone { get; set; }

        public IEnumerable<Disciplina> Disciplina { get; set; }
    }
}