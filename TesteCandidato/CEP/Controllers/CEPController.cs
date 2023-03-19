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
using CEP.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CEP.Controllers {
    public class CEPController:Controller {

        private readonly IRepositorioCep servicesCep;
        private readonly IConfiguration configuration;

        public CEPController (IRepositorioCep servicesCep, IConfiguration configuration) {
            this.servicesCep = servicesCep;
            this.configuration = configuration;
        }

        public IActionResult ValidarCEP () {
            return View();
        }

        [HttpGet]
        public IActionResult BuscarPorCodigo (string cep) {
            if(!string.IsNullOrEmpty(cep)) {
                using var client = new RestClient("https://viacep.com.br");
                var request = new RestRequest($"/ws/{cep}/json/", Method.Get);

                var response = client.Execute<CEPS>(request);

                if(response.IsSuccessful) {
                    if(response.Data?.Erro ?? false) {
                        ViewBag.Messagem = "CEP não encontrado";
                        return View("ValidarCEP");
                    }
                    return View("ExibirEndereco", response.Data);
                } else {
                    return NotFound();
                }
            } else {
                return NotFound();
            }
        }

        public IActionResult ExibirEndereco (string id) {
            if(!string.IsNullOrEmpty(id)) {
                ViewBag.visualizar = true;
                CEPS tempCep = servicesCep.CepRepositorio(id).Result;
                return View(tempCep);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Gravar (CEPS ceps) {
            if(!string.IsNullOrEmpty(ceps.Cep)) {
                CEPS tempCep = await servicesCep.CepRepositorio(ceps.Cep);
                if(tempCep == null) 
                    await servicesCep.GravarCep(ceps);
            }
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Listar () {
            var ceps = servicesCep.ListarCep().Result;
            ViewBag.Cep = ceps;
            return View();
        }
    }
}
