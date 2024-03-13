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
        private readonly DataContext _context;

        public ProfessorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(_context.Professores);
        }

        [HttpGet("byId")]
        public IActionResult GetById(int Id) {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == Id);
            if (professor == null) return BadRequest();
            return Ok(professor);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string Nome, string Sobrenome) {
            var professor = _context.Professores.FirstOrDefault(
                a => a.Nome.Contains(Nome) && a.Sobrenome.Contains(Sobrenome)
            );
            if (professor == null) return BadRequest();
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor) {

            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("byId")]
        public IActionResult Put(int Id, Professor professor) {
            var pro = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == Id);
            if (pro == null) return BadRequest("Id não encontrado ou não existe");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("byId")]
        public IActionResult Patch(int Id, Professor professor) {
            var pro = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == Id);
            if (pro == null) return BadRequest("Id não encontrado ou não existe");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("byId")]
        public IActionResult Delete(int Id) {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == Id);
            if (professor == null) return BadRequest("Professor não encontrado ou não existe");

            _context.Remove(professor);
            _context.SaveChanges();
            return Ok("Professor removido com sucesso");
        }
    }
}