using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BuscaCep.Models;

namespace BuscaCep.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConsultaCep([Bind("Cep")] Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { Error = 1, Message = ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)) });
            }

            try
            {
                var correios = new Correios.AtendeClienteClient();

                var consulta = correios.consultaCEPAsync(endereco.Cep).Result;

                if (consulta != null)
                {
                    endereco.Logradouro = consulta.@return.end;
                    endereco.Bairro = consulta.@return.bairro;
                    endereco.Complemento = consulta.@return.complemento2;
                    endereco.Cidade = consulta.@return.cidade;
                    endereco.Uf = consulta.@return.uf;

                    return new JsonResult(new { Error = 0, Result = endereco });
                }

                return new JsonResult(new { Error = 1, Message = "CEP \"" + endereco.Cep + "\" informado não retornou nenhum endereço" });
            }
            catch (Exception)
            {
                return new JsonResult(new { Error = 1, Message = "CEP \"" + endereco.Cep + "\" inválido" });
            }
        }
    }
}
