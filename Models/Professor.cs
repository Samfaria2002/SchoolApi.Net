using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyApiDotNet.Models
{
    public class Professor
    {

        public Professor() {}

        public Professor(int Id, int Registro, string Nome, string Sobrenome, int Telefone) {
            this.Id = Id;
            this.Registro = Registro;
            this.Nome = Nome;
            this.Sobrenome = Sobrenome;
            this.Telefone = Telefone;
        }

        public int Id { get; set; }

        public int Registro { get; set; }
        
        public string Nome { get; set; }

        public string Sobrenome { get; set; }
        
        public int Telefone { get; set; }

        public DateTime DataInicio { get; set; } = DateTime.Now;

        // ? definindo campo nullable
        public DateTime? DataFim { get; set; } = null;

        public bool Ativo { get; set; } = true;

        public IEnumerable<Disciplina> Disciplina { get; set; }
    }
}