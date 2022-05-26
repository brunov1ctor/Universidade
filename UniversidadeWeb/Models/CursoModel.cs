using System;

namespace Universidade.Models
{   
    public class CursoModel
    {
        public long id { get; set;}
        public string nome { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_encerramento { get; set; }
    }
}