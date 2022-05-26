using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface INotasRepositorio
    {
        Task<List<PesquisaNota>> Pesquisar(string search, int ano);
        Task<List<LancarNota>> Lancar(int semestre, int ano, long disciplina_id, long curso_id);
        Task<bool> Salvar(SalvarNota salvar);
    }
     
}