using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Models;
using System;

namespace Universidade.Repositorio 
{
    public interface IAlunoRepositorio
    {
        Task<List<AlunoModel>> Index();
        Task<int> Criar(AlunoModel aluno);
        Task<AlunocomCurso> Atualizar_get(int id);
        Task<int> Atualizar_post(int id, AlunoModel aluno);
        Task<int> Deletar(int Id);
        Task<bool> ValidarCPF(string cpf);
        Task<AlunocomCurso> ModalFiltro(long aluno_id, int ano, int semestre, string search);
        Task<bool> Matricular(long aluno_id, List<long> curso_list, int ano, int semestre);
    }
}