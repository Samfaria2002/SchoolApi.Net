using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyApiDotNet.Data;
using UdemyApiDotNet.Models;
using Microsoft.EntityFrameworkCore;
using UdemyApiDotNet.Dtos;
using AutoMapper;

namespace UdemyApiDotNet.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {

        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // api/aluno
        [HttpGet]
        public IActionResult Get() {
            var response = _repo.GetAllAlunos(false);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(response));
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
            var aluno = _repo.GetAlunoById(Id, false);
            if (aluno == null) return BadRequest();

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome) {
            var aluno = _repo.GetAlunoByName(nome, sobrenome, false);
            /*var aluno = _context.Alunos.FirstOrDefault(
                a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
                );
            */
            if (aluno == null) return BadRequest();
            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model) {
            
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);
            if (_repo.SaveChanges()) {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado");
        }

        // api/aluno/byId?id={id} 
        /*Ao alterar um objeto json pelo put, deve se passar 
        o id também, senão ele cria um objeto igual, modificado porém 
        com outro id, ou seja, um outro objeto*/
        [HttpPut("byId")]
        public IActionResult Put(int id, AlunoRegistrarDto model) {

            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, alu);

            _repo.Update(alu);
            if (_repo.SaveChanges()) {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(alu));
            }
            return BadRequest("Aluno não cadastrado");
        }

        // api/aluno/byId?id={id}
        [HttpPatch("byId")]
        public IActionResult Patch(int id, AlunoRegistrarDto model) {

            /*AsNoTracking serve para evitar a redundância 
            de passar o id pela rota (url) e pelo obj json*/
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, alu);

            _repo.Update(alu);
            if (_repo.SaveChanges()) {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(alu));
            }
            return BadRequest("Aluno não cadastrado");
        }

        // api/aluno/byId?id={id}
        [HttpDelete("byId")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges()) {
                return Ok("Aluno deletado");
            }
            return BadRequest("Aluno não deletado");
        }
    }
}