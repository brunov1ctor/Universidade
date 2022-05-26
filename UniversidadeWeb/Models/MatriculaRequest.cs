using System.Collections.Generic;

namespace Universidade.Models
{   
    public class MatriculaRequest
    {
        public long aluno_id{get; set;}
        public List<long> curso_list{get; set;}
        public int ano{get; set;}
        public int semestre{get; set;}   
    }
}