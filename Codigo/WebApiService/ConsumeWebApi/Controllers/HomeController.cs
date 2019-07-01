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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Index", "Doctos");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
        
    }
}