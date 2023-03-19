using CEP.Interface;
using CEP.Models;

namespace CEP.Service
{
    public class ServicesCep : ISerciesCep
    {
        private readonly IRepositorioCep repositorioCep;

        public async Task<bool> CepExiste (string cep) {
            return await repositorioCep.CepExiste (cep);
        }

        public async Task<CEPS> CepRepositorio(string cep)
        {
            return await repositorioCep.CepRepositorio(cep);
        }

        public async Task GravarCep(CEPS cep)
        {
            await repositorioCep.GravarCep(cep);
        }

        public async Task<List<CEPS>> ListarCep () {
            return await repositorioCep.ListarCep();
        }
    }
}
