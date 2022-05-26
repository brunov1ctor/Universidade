using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface IAprovacaoCursoRepositorio
    {
        Task<List<CursoModel>> Index();
        Task <List<AprovacaoCursoModel>> Pesquisar(string curso, int ano, int semestre);
    }
}