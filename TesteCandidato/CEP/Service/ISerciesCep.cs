using CEP.Interface;
using CEP.Models;

namespace CEP.Service
{
    public interface ISerciesCep : IRepositorioCep
    {
        Task<CEPS> CepRepositorio(string cep);
        Task<bool> CepExiste (string cep);
        Task<List<CEPS>> ListarCep ();
        Task GravarCep(CEPS cep);
    }
}
