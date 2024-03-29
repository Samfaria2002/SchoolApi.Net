using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyApiDotNet.Dtos
{
    public class AlunoRegistrarDto
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;

        // ? definindo campo nullable
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
    }
}