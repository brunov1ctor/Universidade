using System;

namespace Universidade.Models
{   
    public class AlunoModel
    {
        public int id { get; set;}
        public string nome { get; set; }
        public DateTime data_nascimento { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string cpf { get; set; }    
    }
}