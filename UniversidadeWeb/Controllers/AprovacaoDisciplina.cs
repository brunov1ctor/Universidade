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
    public class AprovacaoDisciplina : Controller
    {
        private IAprovacaoDisciplinaRepositorio _aprovacaodisciplinarepositorio;
        private readonly ILogger<HomeController> _logger;

        public AprovacaoDisciplina(ILogger<HomeController> logger, IAprovacaoDisciplinaRepositorio aprovacaodisciplinarepositorio)
        {
            _aprovacaodisciplinarepositorio = aprovacaodisciplinarepositorio;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var l = await _aprovacaodisciplinarepositorio.Index(); //pega a lista no banco
            return View(l);
        }
        public async Task<IActionResult> Pesquisar(string curso, int ano, int semestre)
        {
            var l = await _aprovacaodisciplinarepositorio.Pesquisar(curso, ano, semestre); 
            return PartialView("pesquisa",l);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
