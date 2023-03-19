using CEP.Interface;
using CEP.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CEP.Repository {
    public class CEPRepository : IRepositorioCep {

        private readonly string connectionString;

        public CEPRepository(IConfiguration configuration) {

            this.connectionString = configuration.GetConnectionString("connectionString");

        }

        public async Task<bool> CepExiste (string cep) {
            CEPS ceps = await CepRepositorio(cep);
            if(ceps != null) {
                return true;
            } else {
                return false;
            }

        }

        public async Task<CEPS> CepRepositorio (string cep) {
            using(var conexao = new SqlConnection(connectionString)) {
                await conexao.OpenAsync();
                var result = await conexao.QueryFirstOrDefaultAsync<CEPS>("SELECT * FROM Cep WHERE cep = @cep", new { cep });
                return result;
            }
        }

        public async Task<List<CEPS>> ListarCep () {
            using(var conexao = new SqlConnection(connectionString)) {
                await conexao.OpenAsync();
                var result = await conexao.QueryAsync<CEPS>("SELECT * FROM Cep");
                return result.ToList();
            }
        }

        public async Task GravarCep (CEPS cep) {
            try {

                string sql = @"
                    INSERT INTO 
                        [dbo].[CEP] ([cep], [logradouro], [complemento], [bairro], [localidade], [uf], [unidade], [ibge], [gia]) 
                    VALUES 
                        (@cep, @logradouro, @complemento, @bairro, @localidade, @uf, @unidade, @ibge, @gia) ";

                using SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@cep", cep.Cep));
                cmd.Parameters.Add(new SqlParameter("@logradouro", (object)cep.Logradouro ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@complemento", (object)cep.Complemento ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@bairro", (object)cep.Bairro ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@localidade", (object)cep.Localidade ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@uf", (object)cep.UF ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@unidade", (object)cep.Unidade ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ibge", (object)cep.IBGE ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@gia", (object)cep.GIA ?? DBNull.Value));

                await cmd.ExecuteScalarAsync();
                

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
