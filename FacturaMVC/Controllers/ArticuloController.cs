using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using FacturaMVC.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
//using System.Web.Mvc;

namespace FacturaMVC.Controllers
{
    public class ArticuloController : Controller
    {

        private static string _baseUrl;

        public ArticuloController()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                       
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }
        // GET: ArticuloController
        public  async   Task<ActionResult> Index()
        {            
                List<Articulo> lista = new List<Articulo>();

                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(_baseUrl);
                //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = await cliente.GetAsync("api/Articulo");

                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<List<Articulo>>(json_respuesta);
                    lista = resultado;
                }        

            return View(lista);
        }

		
		public async  Task<ActionResult> BuscarArticulo(int id)
		{
			Articulo articulo = new Articulo();

			var cliente = new HttpClient();
			cliente.BaseAddress = new Uri(_baseUrl);
			//cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
			var response = await cliente.GetAsync($"api/Articulo/{id}");

			if (response.IsSuccessStatusCode)
			{
				var json_respuesta = await response.Content.ReadAsStringAsync();
				var resultado = JsonConvert.DeserializeObject<Articulo>(json_respuesta);
				articulo = resultado;
			}

			return new JsonResult(new { success = true, results = articulo });
			//return JsonResult(new {success = true, results = articulo}, JsonRequestBehavior.AllowGet);

			//return View(lista);
		}


		// GET: ArticuloController/Create
		public ActionResult Create()
        {
            return View();
        }

        // POST: ArticuloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(Articulo articulo)
        {
            try
            {
                bool respuesta = false;                


                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(_baseUrl);
                //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var content = new StringContent(JsonConvert.SerializeObject(articulo), Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync("api/Articulo/", content);

                if (response.IsSuccessStatusCode)
                {
                    respuesta = true;
                }

                //return respuesta;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticuloController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Articulo articulo = new Articulo();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Articulo/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Articulo>(json_respuesta);
                articulo = resultado;
            }

            return View(articulo);
        }

        // POST: ArticuloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Articulo articulo)
        {
            try
            {
                bool respuesta = false;

                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(_baseUrl);
                //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var content = new StringContent(JsonConvert.SerializeObject(articulo), Encoding.UTF8, "application/json");

                var response = await cliente.PutAsync("api/Articulo/", content);

                if (response.IsSuccessStatusCode)
                {
                    respuesta = true;
                }
                //return respuesta;
                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticuloController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Articulo articulo = new Articulo();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Articulo/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Articulo>(json_respuesta);
                articulo = resultado;
            }

            return View(articulo);            
        }

        // POST: ArticuloController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> ConfirmaAnulacion( int articuloId)
        {
            try
            {
               

                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(_baseUrl);
                //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = await cliente.DeleteAsync ($"api/Articulo/{articuloId}");
               
                    return RedirectToAction(nameof(Index));               

            }
            catch
            {
                return View();
            }
        }
    }
}
