using Universidade.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Universidade.Models;

namespace Universidade.Repositorio
{
    public class DisciplinaRepositorio : IDisciplinaRepositorio
    {
        private DbSession _db;

        public DisciplinaRepositorio(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Criar(DisciplinasemId d)
        {
            using (var conn = _db.Connection)
            {
                string query = @"INSERT INTO disciplina (nome,carga_horaria) 
                values(@nome,@carga_horaria)";

                var result = await conn.ExecuteAsync(sql: query, param: new{d.nome, d.carga_horaria});
                return result;
            }
        }
        public async Task<List<DisciplinaModel>> Listar()
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM disciplina";
                List<DisciplinaModel> l = (await conn.QueryAsync<DisciplinaModel>(sql: query)).ToList();
                return l;
            }
        }
        public async Task<int> Atualizar(int id, DisciplinasemId d)
        {
            using (var conn = _db.Connection)
            {
                string query = @"UPDATE disciplina SET carga_horaria = @carga_horaria
                 WHERE id = @id";

                var result = await conn.ExecuteAsync(sql:query, param:new{d.nome, d.carga_horaria});
                return result;
            }
        }
        public async Task<int> Deletar(int id)
        {
            using (var conn = _db.Connection)
            {
                
                try{
               string query1 = @"DELETE FROM curso_disciplina WHERE disciplina_id = @id";

                var result1 = await conn.ExecuteAsync(sql: query1, param: new{id});

                string query2 = @"DELETE FROM disciplina WHERE id = @id";

                var result2 = await conn.ExecuteAsync(sql: query2, param: new{id});

                return result2;
                }catch(System.Exception e){
                    throw e;

                }

            }
        }
        public async Task<bool> ValidarNome(string nome) //validar nome da disciplina
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM disciplina WHERE nome = @nome";
                List<DisciplinaModel> l = (await conn.QueryAsync<DisciplinaModel>(sql: query, param:new{nome})).ToList();

                if(l.Count>0)  //verificando se já existe na tabela caso já retorna falso
                    return false;
                
                return true; //o nome é valido
            }
        }
        public async Task<bool> ValidarAluno(int id) 
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM aluno_disciplina WHERE disciplina_id = @id";
                List<DisciplinaModel> l = (await conn.QueryAsync<DisciplinaModel>(sql: query, param:new{id})).ToList();

                if(l.Count>0)  //verificando se existe aluno na disciplina caso já retorna falso
                    return false;
                
                return true; //exclusão é valida
            }
        }

        public async Task<bool> ValidarCurso(int id) 
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM curso_disciplina WHERE disciplina_id = @id";
                List<DisciplinaModel> l = (await conn.QueryAsync<DisciplinaModel>(sql: query, param:new{id})).ToList();

                if(l.Count>0)  //verificando se existe curso com a disciplina caso já retorna falso
                    return false;
                
                return true; //exclusão é valida
            }
        }
    }
}
