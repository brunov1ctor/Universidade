using System;

namespace Universidade.Models
{   
    public class MatriculaModel
    {
        public int id { get; set;}
        public long curso_id { get; set; }
        public long aluno_id { get; set; }
        public int ano { get; set; }
        public int semestre { get; set; }    
    }
}