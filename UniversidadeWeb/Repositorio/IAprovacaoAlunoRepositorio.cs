using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface IAprovacaoAlunoRepositorio
    {
        Task<List<CursoModel>> Index();
        Task <List<AprovacaoAlunoModel>> Pesquisar(string curso, int ano, int semestre, string cpf);
    }
}