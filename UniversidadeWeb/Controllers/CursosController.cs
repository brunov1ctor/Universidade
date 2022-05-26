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
    public class CursosController : Controller
    {
        private ICursoRepositorio _cursorepositorio;
        private readonly ILogger<HomeController> _logger;

        public CursosController(ILogger<HomeController> logger, ICursoRepositorio cursorepositorio)
        {
            _cursorepositorio = cursorepositorio;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var l = await _cursorepositorio.Index(); //pega a lista no banco
            return View(l);
        }

        public IActionResult Inserir()
        {
            return View();
        }
        public async Task<JsonResult> Criar(CursoModel curso)
        {
            var result = await _cursorepositorio.Criar(curso);
            if(result>0){
                return Json(new{valido = true});
            }else{
                return Json(new{valido = false});
            } 
        }

        public async Task<IActionResult> Atualizar_get(int id)
        {
            CursocomDisciplinas cursocomdisciplinas = await _cursorepositorio.Atualizar_get(id);  //pega o curso com o id passado
            return View(cursocomdisciplinas);
        }

        public IActionResult Atualizar_post(int id,CursoModel curso)
        {
            _cursorepositorio.Atualizar_post(id,curso);
            return RedirectToAction("Index");
        }
        public async Task<JsonResult> Deletar(int id)
        {        
            var result = await _cursorepositorio.Deletar(id);
            if(result>0){
                return Json(new{valido = true});
            }else{
                return Json(new{valido = false});
            } 
        }

        public async Task<JsonResult> ValidaNome(string nome)
        { 
            bool valido = await _cursorepositorio.ValidarNome(nome);
            return Json(new { valido});
        }

        public async Task<JsonResult> ValidaExclusao(int id)
        { 
            bool valido = await _cursorepositorio.ValidarExclusao(id);
            return Json(new { valido});
        }

        public async Task<IActionResult> Modalfiltro(long curso_id, string search)
        { 
            try{
                CursocomDisciplinas cursocomdisciplinasfaltantes = await _cursorepositorio.ModalFiltro(curso_id, search);
                return PartialView("_modalpesquisa",cursocomdisciplinasfaltantes); //aqui é adicionado a div com as disciplinas faltantes
    
            }catch (Exception e){
                throw e;
            }        
        }

        public async Task<JsonResult> IncluirDisciplina(long curso_id, long disciplina_id)
        {
            bool valido = await _cursorepositorio.IncluirDisciplina(curso_id, disciplina_id);  //inclui disc no curso com o id passado
            return Json(new { valido});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
