using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface ICursoRepositorio
    {
        Task<List<CursoModel>> Listar();
        Task<int> Criar(CursosemId curso);
        Task<bool> ValidarNome(string nome);
        Task<int> Atualizar(int id,CursosemId curso);
        Task<int> Deletar(int Id);
        Task<bool> ValidarExclusao(int id);
        Task<bool> IncluirDisciplina(long curso_id, long disciplina_id);
    }
}