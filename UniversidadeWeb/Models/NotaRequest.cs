using System.Collections.Generic;

namespace Universidade.Models
{   
    public class NotaRequest
    {
        public long curso_id{get; set;}
        public long disciplina_id{get; set;}
        public int ano{get; set;}
        public int semestre{get; set;}   
    }
}