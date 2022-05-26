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
    public class DisciplinasController : Controller
    {
        private IDisciplinaRepositorio _disciplinarepositorio;
        private readonly ILogger<HomeController> _logger;

        public DisciplinasController(ILogger<HomeController> logger, IDisciplinaRepositorio disciplinarepositorio)
        {
            _disciplinarepositorio = disciplinarepositorio;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _disciplinarepositorio.Index(); //pega a lista no banco
            return View(result);
        }

        public IActionResult Inserir()
        {
            return View();
        }
        public async Task<JsonResult> Criar(DisciplinaModel disciplina)
        {
            var result = await _disciplinarepositorio.Criar(disciplina);
            if(result>0)
                return Json(new{valido=true});
            else
                return Json(new{valido=false});
        }

        public IActionResult Atualizar_get(int id)
        {
            DisciplinaModel disciplina = _disciplinarepositorio.Atualizar_get(id);  //pega o disciplina com o id passado
            return View(disciplina);
        }

        public IActionResult Atualizar_post(int id,DisciplinaModel disciplina)
        {
            _disciplinarepositorio.Atualizar_post(id,disciplina);
            return RedirectToAction("Index");
        }
        public async Task<JsonResult> Deletar(int id)
        {   
            var result = await _disciplinarepositorio.Deletar(id); 
            if(result>0)       
                return Json(new{valido=true});
            else
                return Json(new{valido=false});
        }
        public async Task<JsonResult> ValidaNome(string nome)
        { 
            bool valido = await _disciplinarepositorio.ValidarNome(nome);
            return Json(new { valido});
        }
        [HttpGet]
        public async Task<JsonResult> ValidaExclusao(int id)
        { 
            bool validar_aluno = await _disciplinarepositorio.ValidarAluno(id);

            bool validar_curso = await _disciplinarepositorio.ValidarCurso(id);

            return Json(new { validar_aluno,validar_curso});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
