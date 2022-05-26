using System.Collections.Generic;

namespace Universidade.Models
{   
    public class SalvarNota
    {
        public long curso_id{get; set;}
        public long disciplina_id{get; set;}
        public List<InserirNota> notas{get; set;}
    }
}