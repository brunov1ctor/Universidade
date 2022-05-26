using System.Collections.Generic;

namespace Universidade.Models
{   
    public class PesquisaNota
    {
        public long curso_id{get; set;}
        public string curso{get; set;}
        public long disciplina_id{get; set;}
        public string disciplina{get; set;}        
        public int ano{get; set;}
        public int semestre{get; set;}   
    }
}