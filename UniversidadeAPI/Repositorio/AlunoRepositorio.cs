using Universidade.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Universidade.Models;
using Validacao;

namespace Universidade.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private DbSession _db;

        public AlunoRepositorio(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Criar(AlunosemId novo_aluno)
        {
            using (var conn = _db.Connection)
            {
                string query = @"INSERT INTO aluno (nome,data_nascimento,email,telefone,cpf) 
                values(@nome,@data_nascimento,@email,@telefone,@cpf)";

                var result = await conn.ExecuteAsync(sql: query, param:new{novo_aluno.nome, novo_aluno.data_nascimento, novo_aluno.email, novo_aluno.cpf});
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
        public async Task<List<AlunoModel>> Listar()
        {
           try{
                    using (var conn = _db.Connection)
                {
                    string query = @"SELECT * FROM aluno ORDER BY data_nascimento, email";
                    List<AlunoModel> l = (await conn.QueryAsync<AlunoModel>(sql: query)).ToList();
                    return l;
                }
           }catch(System.Exception e){
               throw e;
           }
        }

        public async Task<int> Atualizar(int id, AlunosemId aluno)
        {
            using (var conn = _db.Connection)
            {   
                    string query = @"UPDATE aluno SET nome = @nome, data_nascimento = @data_nascimento,
                    email = @email WHERE id = @id";
                    var result = await conn.ExecuteAsync(sql: query, param:new{id, aluno.nome, aluno.data_nascimento, aluno.email, aluno.cpf});
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


    }
}
