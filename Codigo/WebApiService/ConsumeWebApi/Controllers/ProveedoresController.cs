using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsumeWebApi.Models;
using System.Net.Http;

namespace ConsumeWebApi.Controllers
{
    [Authorize]
    public class ProveedoresController : Controller
    {
        IEnumerable<ProveedoresViewModels> Proveedor = null;
        string urlApiProveedor = System.Configuration.ConfigurationManager.AppSettings["apiaxosnet.proveedores"];
        // GET: Proveedores
        public ActionResult Index()
        {
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
            }
            return View(Proveedor);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            ProveedoresViewModels Proveedor = new ProveedoresViewModels();
            return View(Proveedor);
        }

        // POST: Proveedores/Create
        [HttpPost]
        public ActionResult Create(ProveedoresViewModels model)
        {
            try
            {
                 using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri(urlApiProveedor);
                    var responseTask = client.PostAsJsonAsync<ProveedoresViewModels>("Insert", model);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }


                }

                Proveedor = Enumerable.Empty<ProveedoresViewModels>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        
    }
}
