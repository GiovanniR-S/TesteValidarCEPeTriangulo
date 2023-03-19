using CEP.Models;

namespace CEP.Interface
{
    public interface IRepositorioCep
    {
        Task<CEPS> CepRepositorio(string cep);
        Task<bool> CepExiste(string cep);
        Task<List<CEPS>> ListarCep();
        Task GravarCep(CEPS cep);
    }
}
