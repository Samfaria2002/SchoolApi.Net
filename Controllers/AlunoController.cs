using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyApiDotNet.Data;
using UdemyApiDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace UdemyApiDotNet.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly DataContext _context;

        public AlunoController(DataContext context)
        {
            _context = context;
        }

        // api/aluno
        [HttpGet]
        public IActionResult Get() {
            return Ok(_context.Alunos);
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
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == Id);
            if (aluno == null) return BadRequest();
            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome) {
            var aluno = _context.Alunos.FirstOrDefault(
                a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
                );
            if (aluno == null) return BadRequest();
            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno) {

            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // api/aluno/{id} 
        /*Ao alterar um objeto json pelo put, deve se passar 
        o id também, senão ele cria um objeto igual, modificado porém 
        com outro id, ou seja, um outro objeto*/
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno) {

            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado");
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // api/aluno/{id}
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno) {

            /*AsNoTracking serve para evitar a redundância 
            de passar o id pela rota (url) e pelo obj json*/
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado");
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // api/aluno/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("Aluno não encontrado");
            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok();
        }
    }
}