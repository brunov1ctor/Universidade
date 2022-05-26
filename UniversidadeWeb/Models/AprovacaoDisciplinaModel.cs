namespace Universidade.Models
{   
    public class AprovacaoDisciplinaModel
    {
        public string nome_aluno { get; set; }
        public string cpf { get; set; }   
        public decimal? nota { get; set;} 
        public bool aprovado {get; set;}
        public string nome_disciplina { get; set; }
    }
}