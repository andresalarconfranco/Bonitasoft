using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiAfiliaciones.Dal
{
    public class AfiliacionesDal
    {
        private AfiliacionesSkandiaEntities AfiliacionesSkEntities { get; set; }

        public List<Afiliacion> ObtenerAfiliaciones()
        {
            try
            {
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    return AfiliacionesSkEntities.Afiliacion.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Afiliacion ObtenerAfiliacionPorIdAfiliacion(int idAfiliacion)
        {
            try
            {
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    return AfiliacionesSkEntities.Afiliacion.FirstOrDefault(x => x.IdAfiliacion == idAfiliacion);
                }
            }
            catch  (Exception ex)
            {
                throw;
            }
        }

        public Afiliacion ObtenerAfiliacionPorIdCliente(int idCliente)
        {
            try
            {
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    return AfiliacionesSkEntities.Afiliacion.FirstOrDefault(x => x.IdCliente == idCliente);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Afiliacion ActualizarAfiliacion(Afiliacion afiliacion)
        {
            try
            {
                var afil = new Afiliacion()
                {
                    IdAfiliacion = -1
                };

                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    var afilAct = AfiliacionesSkEntities.Afiliacion.FirstOrDefault(x => x.IdAfiliacion == afiliacion.IdAfiliacion);
                    afilAct.EstadoAfiliacion = afiliacion.EstadoAfiliacion;
                    AfiliacionesSkEntities.SaveChanges();
                }

                afil = this.ObtenerAfiliacionPorIdAfiliacion(afiliacion.IdAfiliacion);

                return afil;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Afiliacion CrearAfiliacion(Afiliacion afiliacion)
        {
            try
            {
                var afil = new Afiliacion()
                {
                    IdAfiliacion = -1
                };
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    AfiliacionesSkEntities.Afiliacion.Add(afiliacion);
                    AfiliacionesSkEntities.SaveChanges();
                }

                return this.ObtenerAfiliacionPorIdAfiliacion(afiliacion.IdAfiliacion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}