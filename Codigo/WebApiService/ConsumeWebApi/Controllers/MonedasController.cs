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
    public class MonedasController : Controller
    {
        IEnumerable<MonedasViewModel> Monedas = null;
        string urlApiMoneda = System.Configuration.ConfigurationManager.AppSettings["apiaxosnet.monedas"];

        //MonedasViewModel Monedas = null;
        // GET: Monedas
        public ActionResult Index()
        {
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
            }
            return View(Monedas);
        }

        // GET: Monedas/Create
        public ActionResult Create()
        {
            MonedasViewModel Monedas = new MonedasViewModel();
            return View(Monedas);
        }

        // POST: Monedas/Create
        [HttpPost]
        public ActionResult Create(MonedasViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                   /* MonedasViewModel Moneda = new MonedasViewModel()
                    {
                        Nombre = model.Nombre,
                        TextoImpteLetra = model.TextoImpteLetra,
                        Simbolo = model.Simbolo,
                        FechaHoraCreacion = model.FechaHoraCreacion,
                        FechaHoraModificacion = model.FechaHoraModificacion
                    };*/
                    client.BaseAddress = new Uri(urlApiMoneda);
                    var responseTask=client.PostAsJsonAsync<MonedasViewModel>("Insert", model);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }


                }

                Monedas = Enumerable.Empty<MonedasViewModel>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Monedas/Edit/5
        public ActionResult Edit(int id)
        {
            MonedasViewModel money= null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApiMoneda);
                //HTTP GET
                //var responseTask = client.GetAsync("student?id=" + id.ToString());
                var responseTask = client.GetAsync("Get/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //var readTask = result.Content.ReadAsAsync<IList<MonedasViewModel>>();
                    var readTask = result.Content.ReadAsAsync<MonedasViewModel>();
                    readTask.Wait();

                    money = readTask.Result;
                }
            }

            return View(money);
        }

        // POST: Monedas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MonedasViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApiMoneda);

                    //HTTP POST
                    var putTask = client.PostAsJsonAsync<MonedasViewModel>("update", model);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }

                Monedas = Enumerable.Empty<MonedasViewModel>();
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
