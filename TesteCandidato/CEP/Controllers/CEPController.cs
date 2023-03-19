using CEP.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using CEP.Models;
using RestSharp;
using System.Runtime.ConstrainedExecution;

namespace CEP.Controllers {
    [Route("Cep")]
    public class CEPController:Controller {

        public IActionResult ValidarCEP () {
            return View();
        }

        [HttpGet("{cep}")]
        public IActionResult BuscarPorCodigo (string cep) {
            var client = new RestClient("https://viacep.com.br");
            var request = new RestRequest($"/ws/{cep}/json/", Method.Get);

            var response = client.Execute<CEPS>(request);

            if(response.IsSuccessful) {
                return Ok(response.Data);
            } else {
                return NotFound();
            }
        }
    }
}
