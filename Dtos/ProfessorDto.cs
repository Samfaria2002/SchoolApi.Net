using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyApiDotNet.Dtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }

        public int Registro { get; set; }
        
        public string Nome { get; set; }

        public string Sobrenome { get; set; }
        
        public int Telefone { get; set; }

        public DateTime DataInicio { get; set; }

        // ? definindo campo nullable

        public bool Ativo { get; set; } = true;

    }
}