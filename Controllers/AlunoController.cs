using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyApiDotNet.Models;

namespace UdemyApiDotNet.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {

        public List<Aluno> Alunos = new List<Aluno>() {
            new Aluno() {
                Id = 1,
                Nome = "Marcos",
                Sobrenome = "Fernandes",
                Telefone = 123321123
            },
            new Aluno() {
                Id = 2,
                Nome = "Poliana",
                Sobrenome = "Ferraz",
                Telefone = 12123
            },
            new Aluno() {
                Id = 3,
                Nome = "Matheus",
                Sobrenome = "Almeida",
                Telefone = 987281
            },
        };

        public AlunoController()
        {
            
        }

        // api/aluno
        [HttpGet]
        public IActionResult Get() {
            return Ok(Alunos);
        }

        /* [HttpGet("{id:int}")]
            Para acessar: api/aluno/{id} */

        /* Para usar via QueryString, a url ficará assim:
        http://localhost:5001/api/aluno/byId?id=1 */

        [HttpGet("byId")]
        public IActionResult GetById(int Id) {

            /*Essa variável aluno está recebendo o parâmetro id do 
            meu método (que foi passado via rota, ou url). Ela armazena os alunos 
            no "a" do FirstOrDefault e faz uma igualdade onde, o Id da url seja igual ao Id 
            do meu parâmetro "a".
            Caso o id não tenha na lista, ele retorna um BadRequest()*/
            var aluno = Alunos.FirstOrDefault(a => a.Id == Id);
            if (aluno == null) return BadRequest();
            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome) {
            var aluno = Alunos.FirstOrDefault(
                a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
                );
            if (aluno == null) return BadRequest();
            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno) {
            return Ok(aluno);
        }

        // api/aluno/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno) {
            return Ok(aluno);
        }

        // api/aluno/{id}
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno) {
            return Ok(aluno);
        }

        // api/aluno/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}