using ApiAfiliaciones.Dal;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ApiAfiliaciones.Controllers
{
    public class AfiliacionesController : ApiController
    {
        // GET: api/Afiliaciones
        public List<Afiliacion> Get()
        {
            try
            {
                AfiliacionesDal afiliacionesDal = new AfiliacionesDal();

                return afiliacionesDal.ObtenerAfiliaciones();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: api/Afiliaciones/5
        public Afiliacion Get(int idSolicitud)
        {
            try
            {
                AfiliacionesDal afiliacionesDal = new AfiliacionesDal();

                return afiliacionesDal.ObtenerAfiliacionPorIdAfiliacion(idSolicitud);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: api/Afiliaciones
        [HttpPost]
        public Afiliacion Post([FromBody]Afiliacion afiliacion)
        {
            try
            {
                AfiliacionesDal afiliacionesDal = new AfiliacionesDal();
                var afil = afiliacionesDal.CrearAfiliacion(afiliacion);
                return afil;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        public void Put([FromBody]Afiliacion afiliacion)
        {
            try
            {
                AfiliacionesDal afiliacionesDal = new AfiliacionesDal();
                var afil = afiliacionesDal.ActualizarAfiliacion(afiliacion);
                //return afil;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
