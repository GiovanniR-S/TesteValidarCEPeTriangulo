using CEP.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CEP.Repository {
    public class CEPRepository {

        public static CEPS BuscarPorCodigo (string cep) {
            using(var conexao = new SqlConnection("connectionString")) {
                conexao.Open();
                var result = conexao.QueryFirstOrDefault<CEPS>("SELECT * FROM Cep WHERE Codigo = @codigo", new { cep });
                return result;
            }
        }


    }
}
