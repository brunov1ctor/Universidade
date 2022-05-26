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
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private DbSession _db;

        public AlunoRepositorio(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Criar(AlunoModel novo_aluno)
        {
            using (var conn = _db.Connection)
            {
                string query = @"INSERT INTO aluno (nome,data_nascimento,email,telefone,cpf) 
                values(@nome,@data_nascimento,@email,@telefone,@cpf)";

                var result = await conn.ExecuteAsync(sql: query, param: novo_aluno);
                return result;
            }
        }
        public async Task<List<AlunoModel>> Index()
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM aluno ORDER BY data_nascimento, email";
                List<AlunoModel> l = (await conn.QueryAsync<AlunoModel>(sql: query)).ToList();
                return l;
            }
        }


        public async Task<AlunocomCurso> Atualizar_get(int id)
        {
            using (var conn = _db.Connection)
            {
                
                string query = @"SELECT * FROM aluno WHERE id = @id;
                SELECT * FROM curso WHERE id IN (SELECT curso_id FROM matricula WHERE aluno_id = @id)";
                
                var reader = await conn.QueryMultipleAsync(sql:query,param:new{id});
                return new AlunocomCurso
                {
                    aluno = (await reader.ReadAsync<AlunoModel>()).FirstOrDefault(),
                    cursos = (await reader.ReadAsync<CursoModel>()).ToList()
                };
            
            }
        }

        public async Task<int> Atualizar_post(int id, AlunoModel aluno)
        {
            using (var conn = _db.Connection)
            {   
                    string query = @"UPDATE aluno SET nome = @nome, data_nascimento = @data_nascimento,
                    email = @email WHERE id = @id";
                    var result = await conn.ExecuteAsync(query, aluno);
                    return result;            
            }
        }
        public async Task<int> Deletar(int id)
        {
            using (var conn = _db.Connection)
            {
                string query = @"DELETE FROM aluno WHERE id = @id";

                var result = await conn.ExecuteAsync(sql: query, param: new{id});
                return result;
            }
        }

        public async Task<bool> ValidarCPF(string cpf) //validar cpf do aluno
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM aluno WHERE cpf = @cpf";
                List<AlunoModel> l = (await conn.QueryAsync<AlunoModel>(sql: query, param:new{cpf})).ToList();

                if(l.Count>0)  //verificando se já existe na tabela caso já retorna falso
                    return false;
                
                if(!ValidaCPF.IsCPF(cpf)) //verificando se é valido caso já retorna falso (confio em vc macoratti :) )
                    return false;

                return true; //o cpf é valido
            }
        }

        public async Task<AlunocomCurso> ModalFiltro(long aluno_id, int ano, int semestre, string search)
        {
            using (var conn = _db.Connection)
            {
                List<int> meses;

                if(semestre == 1)
                {
                    meses = new List<int>{1,2,3,4,5,6};
                }else{
                    meses = new List<int>{7,8,9,10,11,12};
                }
                string query = @"SELECT * FROM aluno WHERE id = @aluno_id;                
                SELECT * FROM curso WHERE nome LIKE @search AND 
                id NOT IN (SELECT curso_id FROM matricula WHERE aluno_id = @aluno_id) AND
                MONTH (data_inicio) IN @meses AND
                @ano BETWEEN YEAR(data_inicio) AND YEAR(data_encerramento)";
                
                var reader = await conn.QueryMultipleAsync(sql:query,param:new{aluno_id, ano, meses, search = $"%{search}%"});
                return new AlunocomCurso
                {
                    aluno = (await reader.ReadAsync<AlunoModel>()).FirstOrDefault(),
                    cursos = (await reader.ReadAsync<CursoModel>()).ToList()
                };
            }
        }

        public async Task<bool>Matricular(long aluno_id, List<long> curso_list, int ano, int semestre)
        {
            using (var conn = _db.Connection)
            {
                try{
                    
                    string query = @"INSERT INTO matricula
                                    (curso_id, aluno_id, ano, semestre) VALUES(@curso, @aluno_id, @ano, @semestre);";

                    int cont = 0;
                    foreach (long curso in curso_list){
                    
                    int result = (await conn.ExecuteAsync(sql: query, param:new{curso, aluno_id, ano, semestre}));
                    
                   
                    cont = cont + result;
                    if(cont == curso_list.Count)
                        return true;
                        
                    }
                    return false;
                    
                }catch(Exception e){
                    throw e;
                }
            }
        }

    }
}
