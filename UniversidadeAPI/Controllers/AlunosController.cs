using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http ;
using Universidade.Models;
using Dapper;
using System.Data.SqlClient;
using Universidade.Repositorio; 
using Validacao;

namespace Universidade.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class AlunosController : ControllerBase
    {
        private IAlunoRepositorio _alunorepositorio;
        private readonly ILogger<HomeController> _logger;

        public AlunosController(ILogger<HomeController> logger, IAlunoRepositorio alunorepositorio)
        {
            _alunorepositorio = alunorepositorio;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os alunos
        /// </summary>
        /// <returns>
        /// retorna a lista de alunos 
        /// </returns>
        /// <response code="200">Consulta com sucesso</response>
        /// <response code="400">solicitação não foi processada</response>
        /// <response code="500">Conexão com o banco falhou</response>

        [HttpGet("Listar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
        {
            try{

                var result = await _alunorepositorio.Listar(); //pega a lista no banco
                return Ok(result);

            }catch(Exception){

                return StatusCode(400,
                new{
                    title = "Não foi possível acessar os dados"
                });

            }
        }

        /// <summary>
        /// Criar um aluno
        /// </summary>
        /// <returns>
        /// retorna numero de alunos criado 
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "nome": "Item #1",
        ///        "data_nascimento": data,
        ///        "email": "Item #2"
        ///        "telefone": "Item #3",
        ///        "cpf": "Item #4",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Criado com sucesso</response>
        /// <response code="406">CPF inválido</response>
        
        [HttpPost("Criar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<IActionResult> Criar(AlunosemId aluno)
        {       
            try{          
                if(await _alunorepositorio.ValidarCPF(aluno.cpf))
                {
                    var result = await _alunorepositorio.Criar(aluno);
                    return Ok(result);
                }else{
                    return StatusCode(406,
                        new{
                            title = "CPF invalido não foi possivel criar"
                        }
                    );
                }
            }catch(Exception e){
                return BadRequest();

            }            
        }
        
        /// <summary>
        /// Atualiza um aluno
        /// </summary>
        /// <returns>
        /// retorna o numero de linhas atualizadas 
        /// </returns>
        /// <response code="200">Atualizado com sucesso</response>
        /// <response code="404">Aluno não encontrado</response>
         
        [HttpPut("Atualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Atualizar(int id, AlunosemId aluno)
        {   
            var result = _alunorepositorio.Atualizar(id, aluno);
            return Ok(result);
        }

        /// <summary>
        /// Deleta um aluno
        /// </summary>
        /// <returns>
        /// retorna o numero de linhas deletadas
        /// </returns>
         
        [HttpDelete("Delete/{id}")]
        public IActionResult Deletar(int id)
        {          
            var result  = _alunorepositorio.Deletar(id);
            return Ok(result);
        }

    }
}
