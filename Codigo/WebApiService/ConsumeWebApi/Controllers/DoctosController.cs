using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsumeWebApi.Models;
using System.Net.Http;
using Microsoft.AspNet.Identity;

namespace ConsumeWebApi.Controllers
{
    [Authorize]
    public class DoctosController : Controller
    {
        IEnumerable<DoctosViewModels> Doctos = null;
        string urlApiProveedor = System.Configuration.ConfigurationManager.AppSettings["apiaxosnet.proveedores"];
        string urlApiMoneda = System.Configuration.ConfigurationManager.AppSettings["apiaxosnet.monedas"];
        string urlApiDocto = System.Configuration.ConfigurationManager.AppSettings["apiaxosnet.doctos"];

        private IEnumerable<MonedasViewModel> PrepareMonedas()
        {
            IEnumerable<MonedasViewModel> Monedas = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApiMoneda);

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MonedasViewModel>>();
                    readTask.Wait();

                    Monedas = readTask.Result;
                }
                else
                {
                    //Error response received 
                    Monedas = Enumerable.Empty<MonedasViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
                return Monedas;
            }
        }

        private IEnumerable<ProveedoresViewModels> PrepareProveedores()
        {
            IEnumerable<ProveedoresViewModels> Proveedor = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApiProveedor);

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProveedoresViewModels>>();
                    readTask.Wait();

                    Proveedor = readTask.Result;
                }
                else
                {
                    //Error response received 
                    Proveedor = Enumerable.Empty<ProveedoresViewModels>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
                return Proveedor;
            }
        }
        // GET: Doctos
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApiDocto);

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("Search?UsuarioId="+ User.Identity.GetUserId().ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DoctosViewModels>>();
                    readTask.Wait();

                    Doctos = readTask.Result;
                }
                else
                {
                    //Error response received 
                    Doctos = Enumerable.Empty<DoctosViewModels>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(Doctos);
        }

        

        // GET: Doctos/Create
        public ActionResult Create()
        {
            DoctosViewModels Doctos = new DoctosViewModels();
            var listMon = PrepareMonedas().ToList();
            ViewBag.listMoney = new SelectList(listMon, "MonedaID", "Nombre");

            var listProv = PrepareProveedores().ToList();
            ViewBag.listProv= new SelectList(listProv, "ProveedorID", "Nombre");

            return View(Doctos);
        }

        // POST: Doctos/Create
        [HttpPost]
        public ActionResult Create(DoctosViewModels doctos)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApiDocto);
                    var responseTask = client.PostAsJsonAsync<DoctosViewModels>("Insert", doctos);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                    return RedirectToAction("Index");
            }
            catch
            {
                return View(doctos);
            }
        }

       
    }
}
