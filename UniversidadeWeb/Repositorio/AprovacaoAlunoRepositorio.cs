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
    public class AprovacaoAlunoRepositorio : IAprovacaoAlunoRepositorio
    {
        private DbSession _db;

        public AprovacaoAlunoRepositorio(DbSession dbSession)
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

        public async Task<List<AprovacaoAlunoModel>> Pesquisar(string curso, int ano, int semestre, string cpf)
        {
            try{
                using (var conn = _db.Connection)
                {
                    string query = @"SELECT a.nome nome_aluno, a.cpf, ad.nota ,ad.aprovado, d.nome nome_disciplina, c.nome nome_curso
                                        FROM matricula m
                                        INNER JOIN curso c on m.curso_id = c.id 
                                        INNER JOIN aluno a on m.aluno_id = a.id
                                        INNER JOIN aluno_disciplina ad on a.id = ad.aluno_id 
                                        INNER JOIN disciplina d on ad.disciplina_id = d.id 
                                        WHERE 
                                        c.nome like @curso AND 
                                        m.semestre = @semestre AND 
                                        a.cpf = @cpf AND
                                        @ano BETWEEN YEAR (c.data_inicio) AND YEAR (c.data_encerramento)";
                    List<AprovacaoAlunoModel> l = (await conn.QueryAsync<AprovacaoAlunoModel>(sql: query,param:new{curso, ano, semestre,cpf})).ToList();
                    return l;
                }
            }catch(Exception e){
                throw e;
            }
        }

    }
}
