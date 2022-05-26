using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Repositorio 
{
    public interface IAlunoRepositorio
    {
        Task<List<AlunoModel>> Listar();
        Task<int> Criar(AlunosemId aluno);
        Task<int> Atualizar(int id, AlunosemId aluno);
        Task<int> Deletar(int Id);
        Task<bool> ValidarCPF(string cpf);
    }
}