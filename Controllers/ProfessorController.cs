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
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() {
            var response = _repo.GetAllProfessores(false);
            return Ok(response);
        }

        [HttpGet("byId")]
        public IActionResult GetById(int Id) {
            var professor = _repo.GetProfessorById(Id, false);
            if (professor == null) return BadRequest();
            return Ok(professor);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string Nome, string Sobrenome) {
            /*var professor = _context.Professores.FirstOrDefault(
                a => a.Nome.Contains(Nome) && a.Sobrenome.Contains(Sobrenome)
            );
            */
            var professor = _repo.GetProfessorByName(Nome, Sobrenome, false);
            if (professor == null) return BadRequest();
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor) {

            _repo.Add(professor);
            if (_repo.SaveChanges()) {
                return Ok(professor);
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("byId")]
        public IActionResult Put(int Id, Professor professor) {
            var pro = _repo.GetProfessorById(Id, false);
            if (pro == null) return BadRequest("Id não encontrado ou não existe");

            _repo.Update(professor);
            if (_repo.SaveChanges()) {
                return Ok(professor);
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPatch("byId")]
        public IActionResult Patch(int Id, Professor professor) {
            var pro = _repo.GetProfessorById(Id, false);
            if (pro == null) return BadRequest("Id não encontrado ou não existe");

            _repo.Update(professor);
            if (_repo.SaveChanges()) {
                return Ok(professor);
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