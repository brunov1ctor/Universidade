using System.Collections.Generic;

namespace Universidade.Models
{   
    public class CursoDisciplinasAlunos
    {
        public CursoModel curso {get; set;}
        public List<DisciplinaModel> disciplinas { get; set; }
        public List<AlunoModel> alunos { get; set; }   
    }
}