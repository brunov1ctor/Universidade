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
    public class NotasController : Controller
    {
        private INotasRepositorio _notasrepositorio;
        private readonly ILogger<HomeController> _logger;

        public NotasController(ILogger<HomeController> logger, INotasRepositorio cursorepositorio)
        {
            _notasrepositorio = cursorepositorio;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Pesquisar(string search, int ano)
        {
            var l = await _notasrepositorio.Pesquisar(search, ano); //pega a lista no banco
            return PartialView("pesquisa",l);
        }
        [HttpPost]
        public async Task<IActionResult> LancarNotas([FromBody]NotaRequest nota)
        {
            var l = await _notasrepositorio.Lancar(nota.semestre, nota.ano, nota.disciplina_id, nota.curso_id);
            return PartialView ("modal",l);
        }
        [HttpPost]
        public async Task<JsonResult> Salvar([FromBody]SalvarNota salvar)
        {
            
            var result = await _notasrepositorio.Salvar(salvar);
            
            if(result){
                return Json(new { valido=true});
            }else{
                return Json(new { valido=false});
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
