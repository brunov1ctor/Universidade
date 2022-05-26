using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface IDisciplinaRepositorio
    {
        Task<List<DisciplinaModel>> Index();
        Task<int> Criar(DisciplinaModel d);
        DisciplinaModel Atualizar_get(int id);
        Task<int> Atualizar_post(int id,DisciplinaModel d);
        Task<int> Deletar(int Id);
        Task<bool> ValidarNome(string nome);
        Task<bool> ValidarAluno(int id);
        Task<bool> ValidarCurso(int id);
    }
}