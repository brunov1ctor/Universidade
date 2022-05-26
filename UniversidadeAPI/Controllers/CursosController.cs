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
{   [ApiController]
    [Route("[controller]")]
    public class CursosController : Controller
    {
        private ICursoRepositorio _cursorepositorio;
        private readonly ILogger<HomeController> _logger;

        public CursosController(ILogger<HomeController> logger, ICursoRepositorio cursorepositorio)
        {
            _cursorepositorio = cursorepositorio;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os cursos
        /// </summary>
        /// <returns>
        /// retorna a lista de cursos
        /// </returns>
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var result = await _cursorepositorio.Listar(); //pega a lista no banco
            return Ok(result);
        }

        /// <summary>
        /// Cria um curso
        /// </summary>
        /// <returns>
        /// retorna o numero de cursos criados
        /// </returns>

        [HttpPost("Criar")]
        public async Task<IActionResult> Criar(CursosemId curso)
        {
            if(await _cursorepositorio.ValidarNome(curso.nome)){
                await _cursorepositorio.Criar(curso);
                return Ok();
            }else{
                return BadRequest();
            }
        }
        /// <summary>
        /// Atualiza um cursos
        /// </summary>
        /// <returns>
        /// retorna o numero de linha atualizadas
        /// </returns>
        
        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(int id,CursosemId curso)
        {
            _cursorepositorio.Atualizar(id,curso);
            return Ok();
        }

        /// <summary>
        /// Deleta um curso
        /// </summary>
        /// <returns>
        /// retorna o numero de linhas deletadas
        /// </returns>
        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {        
            if(await _cursorepositorio.ValidarExclusao(id)){
                await _cursorepositorio.Deletar(id);
                return Ok();
            }else{
                return BadRequest();
            }
            
        }

        /// <summary>
        /// Inclui uma nova disciplina no curso
        /// </summary>
        /// <returns>
        /// retorna o numero de disciplinas inseridas
        /// </returns>
        
        [HttpPost("IncluirDisciplina")]
        public async Task<IActionResult> IncluirDisciplina(long curso_id, long disciplina_id)
        {
            await _cursorepositorio.IncluirDisciplina(curso_id, disciplina_id);  //inclui disc no curso com o id passado
            return Ok();
        }

    }
}
