using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyApiDotNet.Models
{
    public class Professor
    {

        public Professor() {}

        public Professor(int Id, string Nome) {
            this.Id = Id;
            this.Nome = Nome;
        }

        public int Id { get; set; }
        
        public string Nome { get; set; }

        public IEnumerable<Disciplina> Disciplina { get; set; }
    }
}