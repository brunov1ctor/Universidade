using System.Collections.Generic;

namespace Universidade.Models
{   
    public class AlunocomCurso
    {
        public AlunoModel aluno { get; set;}
        public List<CursoModel> cursos { get; set; }   
    }
}