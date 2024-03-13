using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UdemyApiDotNet.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public ProfessorController()
        {
            
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok("Professores: Jorge, Renato, Lucia, Mariana");
        }

    }
}