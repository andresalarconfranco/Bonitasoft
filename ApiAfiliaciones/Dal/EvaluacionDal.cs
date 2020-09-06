using System;
using System.Linq;

namespace ApiAfiliaciones.Dal
{
    public class EvaluacionDal
    {
        private AfiliacionesSkandiaEntities AfiliacionesSkEntities { get; set; }

        public Evaluacion ObtenerEvaluacion(string tipoDocumento, string numeroDocumento)
        {
            try
            {
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    return AfiliacionesSkEntities.Evaluacion.FirstOrDefault(x => x.identificationType == tipoDocumento && x.identificationNumber == numeroDocumento);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Evaluacion ObtenerClientePorIdEvaluacion(int idEvaluacion)
        {
            try
            {
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    return AfiliacionesSkEntities.Evaluacion.FirstOrDefault(x => x.IdEvaluacion == idEvaluacion);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}