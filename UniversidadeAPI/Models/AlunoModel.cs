using System;
using Newtonsoft.Json;

namespace Universidade.Models
{   
    public class AlunoModel
    {
        [JsonIgnore]
        public int id { get; set;}
        [JsonRequired]
        public string nome { get; set; }
        [JsonRequired]
        public DateTime data_nascimento { get; set; }
        [JsonRequired]
        public string email { get; set; }
        [JsonRequired]
        public string telefone { get; set; }
        [JsonRequired]
        public string cpf { get; set; }    
    }
}