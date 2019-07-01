using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiService.DataAccessLayer;

namespace WebApiService.Controllers
{
    public class MonedasController : ApiController
    {
        dalDataContext db = new dalDataContext();

        //Esta Metodo retorna todas las monedas
        // GET api/<controller>
        public IEnumerable<Monedas> GetAll()
        {
            return db.Monedas.ToList().AsEnumerable();
        }

        //Este método de acción buscará y filtrará un registro de ID de moneda especifica
        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var MonedaDetalle = (from a in db.Monedas where a.MonedaID == id select a).FirstOrDefault();

            if (MonedaDetalle != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, MonedaDetalle);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Codigo Invalido o no Id no encontrado");
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Insert([FromBody]Monedas _monedas)
        {
            try
            {
                db.Monedas.InsertOnSubmit(_monedas);
                db.SubmitChanges();

                var msg = Request.CreateResponse(HttpStatusCode.Created, _monedas);
                msg.Headers.Location = new Uri(Request.RequestUri + _monedas.MonedaID.ToString());
                return msg;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Para actualizar el registro de monedas
        // PUT api/<controller>/5
        public HttpResponseMessage Update([FromBody]Monedas _monedas)
        {
            var MonedaDetalle = (from a in db.Monedas where a.MonedaID == _monedas.MonedaID select a).FirstOrDefault();
            if (MonedaDetalle != null)
            {
                MonedaDetalle.Nombre = _monedas.Nombre;
                MonedaDetalle.Simbolo = _monedas.Simbolo;
                MonedaDetalle.TextoImpteLetra = _monedas.TextoImpteLetra;
                db.SubmitChanges();
                return Request.CreateResponse(HttpStatusCode.OK, MonedaDetalle);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Codigo Invalido o no Id no encontrado");
            }
        }

    }
}