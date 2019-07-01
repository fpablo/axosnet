using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiService.DataAccessLayer;
using WebApiService.Models;


namespace WebApiService.Controllers
{
    public class DoctosController : ApiController
    {
        dalDataContext db = new dalDataContext();

        //Retorna todos los documentos
        // GET api/<controller>
        public IEnumerable<DoctosModels> GetAll()
        {
            List<DoctosModels> DoctosList = new List<DoctosModels>();

            var query = from doctos in db.Doctos
                        join money in db.Monedas
                        on doctos.MonedaID equals money.MonedaID
                        join proveedor in db.Proveedores
                        on doctos.ProveedorID equals proveedor.ProveedorID
                        select new DoctosModels
                        {
                            DoctoID= doctos.DoctoID,
                            Fecha = doctos.Fecha,
                            Monto = doctos.Monto,
                            ProveedorID = proveedor.ProveedorID,
                            ProveedorNombre = proveedor.Nombre,
                            MonedaID =money.MonedaID,
                            MonedaNombre = money.Nombre,
                            Comentario=doctos.Comentario,
                            FechaHoraCreacion = doctos.FechaHoraCreacion,
                            FechaHoraModificacion =doctos.FechaHoraModificacion
                        };
            DoctosList = query.ToList();
            return DoctosList;
            //return Mapper.Map<IEnumerable<DoctosModels>>(DoctosList);

            //return db.Doctos.ToList().AsEnumerable();

        }

        //Retorna un documento en especifico
        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var DoctosDetalle = (from a in db.Doctos where a.DoctoID == id select a).FirstOrDefault();

            if (DoctosDetalle != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, DoctosDetalle);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Codigo Invalido o no Id no encontrado");
            }
        }

        //Busqueda por usuarios
        // GET api/<controller>/5
        [HttpGet]
        public IEnumerable<DoctosModels> Search(Guid UsuarioId)
        {
            try
            {
                List<DoctosModels> DoctosList = new List<DoctosModels>();

                var query = from doctos in db.Doctos
                            join money in db.Monedas
                            on doctos.MonedaID equals money.MonedaID
                            join proveedor in db.Proveedores
                            on doctos.ProveedorID equals proveedor.ProveedorID
                            where doctos.UsuarioID == UsuarioId
                            select new DoctosModels
                            {
                                DoctoID = doctos.DoctoID,
                                Fecha = doctos.Fecha,
                                Monto = doctos.Monto,
                                ProveedorID = proveedor.ProveedorID,
                                ProveedorNombre = proveedor.Nombre,
                                MonedaID = money.MonedaID,
                                MonedaNombre = money.Nombre,
                                Comentario = doctos.Comentario,
                                FechaHoraCreacion = doctos.FechaHoraCreacion,
                                FechaHoraModificacion = doctos.FechaHoraModificacion
                            };
                DoctosList = query.ToList();
                return DoctosList;

                //return db.Doctos.Where(p=>p.UsuarioID==UsuarioId).ToList().AsEnumerable();
            }
            catch 
            {
                return null;
            }
            
        }

        //Agrega un nuevo registro de Doctos
        // POST api/<controller>
        public HttpResponseMessage Insert([FromBody]Doctos _doctos)
        {
            try
            {
                db.Doctos.InsertOnSubmit(_doctos);
                db.SubmitChanges();

                var msg = Request.CreateResponse(HttpStatusCode.Created, _doctos);
                msg.Headers.Location = new Uri(Request.RequestUri + _doctos.DoctoID.ToString());
                return msg;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        
    }
}