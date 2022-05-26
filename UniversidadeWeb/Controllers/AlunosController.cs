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
    public class AlunosController : Controller
    {
        private IAlunoRepositorio _alunorepositorio;
        private readonly ILogger<HomeController> _logger;

        public AlunosController(ILogger<HomeController> logger, IAlunoRepositorio alunorepositorio)
        {
            _alunorepositorio = alunorepositorio;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _alunorepositorio.Index(); //pega a lista no banco
            return View(result);
        }

        public IActionResult Inserir()
        {
            return View();
        }
        public async Task<JsonResult> Criar(AlunoModel aluno)
        {              
            var result = await _alunorepositorio.Criar(aluno); 
            if(result>0){
                return Json(new { valido=true});
            }else{
                return Json(new { valido=false});
            }
        }

        public async Task<IActionResult> Atualizar_get(int id)
        {
            AlunocomCurso aluno = await _alunorepositorio.Atualizar_get(id);  //pega o aluno com o id passado junto com os cursos
            return View(aluno);
        }

        public IActionResult Atualizar_post(int id, AlunoModel aluno)
        {   
            _alunorepositorio.Atualizar_post(id, aluno);
            return RedirectToAction("Index");
        }
        public IActionResult Deletar(int id)
        {          
            _alunorepositorio.Deletar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<JsonResult> ValidaCpf(string cpf)
        { 
            bool valido = await _alunorepositorio.ValidarCPF(cpf);
            return Json(new { valido});
        }

        public async Task<IActionResult> Modalfiltro(long aluno_id, int ano, int semestre, string search)
        { 
            try{
                AlunocomCurso alunocomcursos = await _alunorepositorio.ModalFiltro(aluno_id, ano, semestre, search);
                return PartialView("_modalpesquisa",alunocomcursos); //aqui é adicionado a div com os cursos
    
            }catch (Exception e){
                throw e;
            }        
        }
        [HttpPost]
        public async Task<JsonResult> Matricular([FromBody]MatriculaRequest matricula)
        { 
            bool valido = await _alunorepositorio.Matricular(matricula.aluno_id, matricula.curso_list, matricula.ano, matricula.semestre);
            return Json(new { valido});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}
