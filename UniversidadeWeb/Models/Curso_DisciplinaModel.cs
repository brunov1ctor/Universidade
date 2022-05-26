using System;

namespace Universidade.Models
{   
    public class Curso_DisciplinaModel
    {
        public long curso_id { get; set;}
        public long disciplina_id { get; set; }
        public DateTime data_desativacao { get; set; }
    }
}