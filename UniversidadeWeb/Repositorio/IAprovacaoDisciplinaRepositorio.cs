using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface IAprovacaoDisciplinaRepositorio
    {
        Task<List<CursoModel>> Index();
        Task <List<AprovacaoDisciplinaModel>> Pesquisar(string curso, int ano, int semestre);
    }
}