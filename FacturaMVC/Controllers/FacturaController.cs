using FacturaMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Collections;

namespace FacturaMVC.Controllers
{
    public class FacturaController : Controller
    {

        private static string _baseUrl;

        public FacturaController()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }
        // GET: FacturaController
        public async Task<ActionResult> Index()
        {
            List<Factura> lista = new List<Factura>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Factura");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Factura>>(json_respuesta);
                lista = resultado;
            }

            return View(lista);
        }

        public async Task<ActionResult> ListaPorCliente(string nombreCliente)
        {
            List<Factura> lista = new List<Factura>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Factura/{nombreCliente}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Factura>>(json_respuesta);
                lista = resultado;
            }

			

			return View("ListFactura",lista);
        }

        public async Task<ActionResult> ListaPorMonto(decimal montoMenor, decimal montoMayor)
        {
            List<Factura> lista = new List<Factura>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Factura/{montoMenor}/{montoMayor}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Factura>>(json_respuesta);
                lista = resultado;
            }

           

            return View("ListFactura",lista);
        }

        // GET: FacturaController/Create
        public ActionResult Create()
        {
            List<FacturaDetalle> ldetalle= new List<FacturaDetalle>();  

            return View();
        }

        // POST: FacturaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(FacturaViewModel facturaVM)
        {
            try
            {

               facturaVM.facturaDetalle.RemoveAt(0);

                var cliente = new HttpClient();     
                cliente.BaseAddress = new Uri(_baseUrl);
                
                //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var content = new StringContent(JsonConvert.SerializeObject(facturaVM), Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync("api/Factura/", content);

                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<Factura>(json_respuesta);
                    facturaVM.factura = resultado;
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Buscar()
        {         

            return View();
        }

    }
}
