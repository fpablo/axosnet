using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiService.DataAccessLayer;

namespace WebApiService.Controllers
{
    public class ProveedoresController : ApiController
    {
        dalDataContext db = new dalDataContext();

        ////Esta Metodo retorna todos los proveedores
        // GET api/<controller>
        public IEnumerable<Proveedores> GetAll()
        {
            return db.Proveedores.ToList().AsEnumerable();
        }

        //Este metodo busca y filtra un registro de ID Proveedor
        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var ProveedorDetalle = (from a in db.Proveedores where a.ProveedorID == id select a).FirstOrDefault();

            if (ProveedorDetalle != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ProveedorDetalle);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Codigo Invalido o no Id no encontrado");
            }
        }

        //Este metodo agrega un nuevo registro de Proveedor
        // POST api/<controller>
        public HttpResponseMessage Insert([FromBody]Proveedores _proveedores)
        {
            try
            {
                db.Proveedores.InsertOnSubmit(_proveedores);
                db.SubmitChanges();

                var msg = Request.CreateResponse(HttpStatusCode.Created, _proveedores);
                msg.Headers.Location = new Uri(Request.RequestUri + _proveedores.ProveedorID.ToString());
                return msg;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Actualiza el registro de Proveedores
        // PUT api/<controller>/5
        public HttpResponseMessage Update(int id, [FromBody]Proveedores _proveedores)
        {
            var ProveedorDetalle = (from a in db.Proveedores where a.ProveedorID == id select a).FirstOrDefault();
            if (ProveedorDetalle != null)
            {
                ProveedorDetalle.Nombre = _proveedores.Nombre;
                db.SubmitChanges();
                return Request.CreateResponse(HttpStatusCode.OK, ProveedorDetalle);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Codigo Invalido o no Id no encontrado");
            }
        }

      
    }
}