using Universidade.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Universidade.Models;
using Validacao;
using System;

namespace Universidade.Repositorio
{
    public class NotasRepositorio : INotasRepositorio
    {
        private DbSession _db;

        public NotasRepositorio(DbSession dbSession)
        {
            _db = dbSession;
        }
        
        public async Task<List<PesquisaNota>> Pesquisar(string search, int ano)
        {
            try{
                using (var conn = _db.Connection)
                {   
                    
                    string query = @"select c.id curso_id , c.nome curso,d.id disciplina_id, 
                                    d.nome disciplina,m.ano, m.semestre from matricula m
                                    inner join curso c on c.id = m.curso_id
                                    inner join curso_disciplina cd on cd.curso_id = c.id
                                    inner join disciplina d on d.id = cd.disciplina_id
                                    where c.nome like @search ";

                    if(ano!= 0){
                        query += "AND ano = @ano ";
                    }

                    query += "group by 1,2,3,4,5,6";

                    List<PesquisaNota> l = (await conn.QueryAsync<PesquisaNota>(sql: query, param:new{ano, search = $"%{search}%"})).ToList();
                    return l;     
                }
            }catch(Exception e){
                throw e;
            }
        }

        public async Task<List<LancarNota>> Lancar(int semestre, int ano, long disciplina_id, long curso_id)
        {
            try{
                using (var conn = _db.Connection)
                {   
                    string query = @"select a.nome nome_aluno,m.id matricula_id, a.id aluno_id , cd.curso_id , cd.disciplina_id, ad.nota,ad.aprovado from matricula m
                    inner join curso_disciplina cd on cd.curso_id = m.curso_id
                    inner join disciplina d on d.id = cd.disciplina_id
                    inner join aluno a on a.id = m.aluno_id
                    left join aluno_disciplina ad on ad.matricula_id = m.id 
                    AND ad.curso_id = cd.curso_id 
                    AND ad.disciplina_id = d.id
                    where m.curso_id = @curso_id
                    and d.id = @disciplina_id;";

                    List<LancarNota> l = (await conn.QueryAsync<LancarNota>(sql: query, param:new{semestre, ano, disciplina_id, curso_id})).ToList();
                    return l;     
                }
            }catch(Exception e){
                throw e;
            }
        }

        public async Task<bool> Get(long curso_id, long disciplina_id, long matricula_id , long aluno_id) //teste se ja existe na tabela aluno_disciplina
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT count(*) as qtd FROM aluno_disciplina 
                        WHERE curso_id = @curso_id AND
                        disciplina_id = @disciplina_id AND
                        matricula_id = @matricula_id AND
                        aluno_id = @aluno_id";
                
                    var r  = (await conn.QueryAsync<VerificaAluno>(sql:query,param:new{curso_id, disciplina_id, matricula_id , aluno_id})).FirstOrDefault();
                    if(r.qtd > 0)
                        return true;                    
                
                return false;
            }
        }        
        public async Task<bool> Salvar(SalvarNota salvar)
        {
            using (var conn = _db.Connection)
            {
                try{
                    
                        foreach(var item in salvar.notas){
                            bool aprovado;
                            if(item.nota > 60){
                                aprovado = true;
                            }else{
                                aprovado = false;
                            }

                            if(await Get(salvar.curso_id,salvar.disciplina_id,item.matricula_id,item.aluno_id)){
                                string query = @"UPDATE aluno_disciplina SET nota=@nota, aprovado=@aprovado 
                                    WHERE curso_id = @curso_id AND
                                    disciplina_id = @disciplina_id AND
                                    matricula_id = @matricula_id AND
                                    aluno_id = @aluno_id";
                                
                                 
                                    var result = await conn.ExecuteAsync(sql:query,param:new{salvar.disciplina_id,item.matricula_id,salvar.curso_id,item.aluno_id,item.nota,aprovado});
                                
                                if(result==salvar.notas.Count)
                                        return true;                    
                                

                            }else{
                                string query = @"INSERT INTO aluno_disciplina (disciplina_id,matricula_id,curso_id,aluno_id,nota,aprovado) 
                                values(@disciplina_id,@matricula_id,@curso_id,@aluno_id,@nota,@aprovado)";
                            
                                
                                    int result = await conn.ExecuteAsync(sql: query, param: new{salvar.disciplina_id,item.matricula_id,salvar.curso_id,item.aluno_id,item.nota,aprovado});
                                    
                                    if(result==salvar.notas.Count)
                                        return true;                                      
                            }

                        }   

                return false;

                }catch(Exception e){
                    throw e;
                }
            }
            
        }

    }
}
