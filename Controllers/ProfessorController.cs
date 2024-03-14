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
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get() {
            var response = _repo.GetAllProfessores(false);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(response));
        }

        [HttpGet("byId")]
        public IActionResult GetById(int Id) {
            var professor = _repo.GetProfessorById(Id, false);
            if (professor == null) return BadRequest();

            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professorDto);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string Nome, string Sobrenome) {
            /*var professor = _context.Professores.FirstOrDefault(
                a => a.Nome.Contains(Nome) && a.Sobrenome.Contains(Sobrenome)
            );
            */
            var professor = _repo.GetProfessorByName(Nome, Sobrenome, false);
            if (professor == null) return BadRequest();

            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professorDto);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model) {

            var professor = _mapper.Map<Professor>(model);
            _repo.Add(professor);
            if (_repo.SaveChanges()) {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("byId")]
        public IActionResult Put(int Id, ProfessorRegistrarDto model) {

            var pro = _repo.GetProfessorById(Id, false);
            if (pro == null) return BadRequest("Id não encontrado ou não existe");

            _mapper.Map(model, pro);

            _repo.Update(pro);
            if (_repo.SaveChanges()) {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(pro));
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPatch("byId")]
        public IActionResult Patch(int Id, AlunoRegistrarDto model) {
            var pro = _repo.GetProfessorById(Id, false);
            if (pro == null) return BadRequest("Id não encontrado ou não existe");

            _mapper.Map(model, pro);

            _repo.Update(pro);
            if (_repo.SaveChanges()) {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(pro));
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpDelete("byId")]
        public IActionResult Delete(int Id) {
            var professor = _repo.GetProfessorById(Id, false);
            if (professor == null) return BadRequest("Professor não encontrado ou não existe");

            _repo.Delete(professor);
            if (_repo.SaveChanges()) {
                return Ok("Professor deletado");
            }
            return BadRequest("Professor não deletado");
        }
    }
}