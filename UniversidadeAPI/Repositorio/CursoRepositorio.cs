using Universidade.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Universidade.Models;
using System;

namespace Universidade.Repositorio
{
    public class CursoRepositorio : ICursoRepositorio
    {
        private DbSession _db;

        public CursoRepositorio(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Criar(CursosemId novo_curso)
        {
            using (var conn = _db.Connection)
            {
                string query = @"INSERT INTO curso (nome,data_inicio,data_encerramento) 
                values(@nome,@data_inicio,@data_encerramento)";

                var result = await conn.ExecuteAsync(sql: query, param: new{novo_curso.nome, novo_curso.data_inicio, novo_curso.data_encerramento});
                return result;
            }
        }
        public async Task<List<CursoModel>> Listar()
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM curso";
                List<CursoModel> l = (await conn.QueryAsync<CursoModel>(sql: query)).ToList();
                return l;
            }
        }

        public async Task<int> Atualizar(int id, CursosemId curso)
        {
            using (var conn = _db.Connection)
            {
                string query = @"UPDATE curso SET nome = @nome WHERE id = @id";

                var result = await conn.ExecuteAsync(sql: query, param: new{curso.nome, curso.data_inicio, curso.data_encerramento});
                return result;
            }
        }
        public async Task<int> Deletar(int id)
        {
            using (var conn = _db.Connection)
            {
                string query = @"DELETE FROM curso WHERE id = @id";

                var result = await conn.ExecuteAsync(sql: query, param: new{id});
                return result;
            }
        }

        public async Task<bool> ValidarNome(string nome) //validar nome do curso
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM curso WHERE nome = @nome";
                List<CursoModel> l = (await conn.QueryAsync<CursoModel>(sql: query, param:new{nome})).ToList();

                if(l.Count>0)  //verificando se já existe na tabela caso já retorna falso
                    return false;
                
                return true; //o nome é valido
            }
        }

        public async Task<bool> ValidarExclusao(int id) //validar nome da disciplina
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM disciplina WHERE id IN (SELECT disciplina_id FROM curso_disciplina WHERE curso_id = @id)";
                List<DisciplinaModel> l = (await conn.QueryAsync<DisciplinaModel>(sql: query, param:new{id})).ToList();

                if(l.Count>0)  //verificando se existe disciplina no curso, caso já retorna falso
                    return false;
                
                return true; //exclusão é valida
            }
        }
        public async Task<bool> IncluirDisciplina(long curso_id, long disciplina_id)
        {
            using (var conn = _db.Connection)
            {
                try{
                DateTime data = DateTime.Now;
                string query = @"INSERT INTO curso_disciplina
                                (curso_id, disciplina_id, data_desativacao) VALUES(@curso_id, @disciplina_id, @data);";

                int result = (await conn.ExecuteAsync(sql: query, param:new{curso_id, disciplina_id, data}));

                if(result>0)
                    return true;
                
                return false;
                }catch(Exception e){
                    throw e;
                }
            }
        }
        
    }
}
