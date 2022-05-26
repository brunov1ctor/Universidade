using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Universidade.Models;
using Dapper;
using System.Data.SqlClient;
using Universidade.Repositorio;

namespace Universidade.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class DisciplinasController : Controller
    {
        private IDisciplinaRepositorio _disciplinarepositorio;
        private readonly ILogger<HomeController> _logger;

        public DisciplinasController(ILogger<HomeController> logger, IDisciplinaRepositorio disciplinarepositorio)
        {
            _disciplinarepositorio = disciplinarepositorio;
            _logger = logger;
        }

        /// <summary>
        /// Lista todas as disciplinas
        /// </summary>
        /// <returns>
        /// retorna a lista de disciplinas
        /// </returns>
        
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var result = await _disciplinarepositorio.Listar(); //pega a lista no banco
            return Ok(result);
        }

        /// <summary>
        /// Cria uma nova disciplina
        /// </summary>
        /// <returns>
        /// retorna o numero de disciplinas criadas
        /// </returns>
        
        [HttpPost("Criar")]
        public async Task<IActionResult> Criar(DisciplinasemId disciplina)
        {
            if(await _disciplinarepositorio.ValidarNome(disciplina.nome)){
                await _disciplinarepositorio.Criar(disciplina);
                return Ok();
            }else{
                return BadRequest();
            }
        }

        /// <summary>
        /// Atualiza uma disciplina
        /// </summary>
        /// <returns>
        /// retorna o numero de linhas atualizadas
        /// </returns>
        
        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(int id,DisciplinasemId disciplina)
        {
            _disciplinarepositorio.Atualizar(id,disciplina);
            return Ok();
        }
        
        /// <summary>
        /// Deleta uma disciplina
        /// </summary>
        /// <returns>
        /// retorna o numero de linhas deletadas
        /// </returns>
        
        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {   
            if(await _disciplinarepositorio.ValidarAluno(id) && await _disciplinarepositorio.ValidarCurso(id)){
                await _disciplinarepositorio.Deletar(id); 
                return Ok();
            }else{
                return BadRequest();
            }
        
        }

    }
}
