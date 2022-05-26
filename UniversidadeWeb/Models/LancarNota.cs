using System.Collections.Generic;

namespace Universidade.Models
{   
    public class LancarNota
    {
        public long matricula_id{get; set;}
        public string nome_aluno{get; set;}
        public decimal? nota{get; set;}
        public bool aprovado{get; set;}
        public long curso_id{get; set;}
        public long disciplina_id{get; set;}
        public long aluno_id{get; set;}
    }
}