using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface ICursoRepositorio
    {
        Task<List<CursoModel>> Index();
        Task<int> Criar(CursoModel curso);
        Task<CursocomDisciplinas> Atualizar_get(int id);
        Task<int> Atualizar_post(int id,CursoModel curso);
        Task<int> Deletar(int Id);
        Task<bool> ValidarNome(string nome);
        Task<bool> ValidarExclusao(int id);
        Task<CursocomDisciplinas> ModalFiltro(long curso_id, string search);
        Task<bool> IncluirDisciplina(long curso_id, long disciplina_id);
    }
}