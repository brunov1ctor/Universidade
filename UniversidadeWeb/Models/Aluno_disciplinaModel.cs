using System;

namespace Universidade.Models
{   
    public class Aluno_disciplina
    {
        public long disciplina_id { get; set;}
        public long matricula_id { get; set; }
        public long curso_id { get; set; }
        public long aluno_id { get; set; }
        public float nota { get; set; }
        public bool aprovado { get; set; }
    }
}