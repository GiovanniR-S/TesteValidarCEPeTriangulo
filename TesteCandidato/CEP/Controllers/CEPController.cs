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
    public class CEPController:Controller {
        // GET: CEPController
        public ActionResult Index () {
            return View();
        }

        // GET: CEPController/Details/5
        public ActionResult Details (int id) {
            return View();
        }

        public IActionResult ValidarCEP () {
            return View();
        }

        [HttpGet("{cep?}")]
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
