using ApiAfiliaciones.Dal;
using System;
using System.Web.Http;

namespace ApiAfiliaciones.Controllers
{
    public class EvaluacionController : ApiController
    {
        [HttpGet]
        public Evaluacion Get(int idEvaluacion)
        {
            try
            {
                EvaluacionDal evaluacionDal = new EvaluacionDal();
                return evaluacionDal.ObtenerClientePorIdEvaluacion(idEvaluacion);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public Evaluacion GetEvaluacion(string documento, string tipo)
        {
            try
            {
                EvaluacionDal evaluacionDal = new EvaluacionDal();
                return evaluacionDal.ObtenerEvaluacion(tipo, documento);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
