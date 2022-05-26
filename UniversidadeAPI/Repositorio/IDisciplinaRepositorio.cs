using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface IDisciplinaRepositorio
    {
        Task<List<DisciplinaModel>> Listar();
        Task<int> Criar(DisciplinasemId d);
        Task<bool> ValidarNome(string nome);
        Task<int> Atualizar(int id,DisciplinasemId d);
        Task<int> Deletar(int Id);
        Task<bool> ValidarAluno(int id);
        Task<bool> ValidarCurso(int id);
    }
}