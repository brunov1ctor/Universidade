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
    public class AprovacaoCursoRepositorio : IAprovacaoCursoRepositorio
    {
        private DbSession _db;

        public AprovacaoCursoRepositorio(DbSession dbSession)
        {
            _db = dbSession;
        }
        
        public async Task<List<CursoModel>> Index()
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM curso";
                List<CursoModel> l = (await conn.QueryAsync<CursoModel>(sql: query)).ToList();
                
                CursoModel selecione = new CursoModel();
                selecione.nome = "--Selecione--";
                l.Insert(0,selecione);
                return l;
            }
        }

        public async Task<List<AprovacaoCursoModel>> Pesquisar(string curso, int ano, int semestre)
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT a.nome, a.cpf, a.data_nascimento 
                                    FROM matricula m
                                    INNER JOIN curso c on m.curso_id = c.id 
                                    INNER JOIN aluno a on m.aluno_id = a.id
                                    WHERE 
                                    c.nome like @curso AND 
                                    m.semestre = @semestre AND 
                                    @ano BETWEEN YEAR (c.data_inicio) AND YEAR (c.data_encerramento)";
                List<AprovacaoCursoModel> l = (await conn.QueryAsync<AprovacaoCursoModel>(sql: query,param:new{curso, ano, semestre})).ToList();
                return l;
            }
        }

    }
}
