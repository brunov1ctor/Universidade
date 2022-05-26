using System.Collections.Generic;

namespace Universidade.Models
{   
    public class CursocomDisciplinas
    {
        public CursoModel curso { get; set;}
        public List<DisciplinaModel> disciplinas { get; set; }   
    }
}