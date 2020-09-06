using System;
using System.Linq;

namespace ApiAfiliaciones.Dal
{
    public class ClienteDal
    {
        private AfiliacionesSkandiaEntities AfiliacionesSkEntities { get; set; }

        public Cliente ObtenerClientePorDocumento(string tipoDocumento, string numeroDocumento)
        {
            try
            {
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    return AfiliacionesSkEntities.Cliente.FirstOrDefault(x => x.TipoDocumento == tipoDocumento && x.NumeroDocumento.ToUpper() == numeroDocumento.ToUpper());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Cliente ObtenerClientePorIdCliente(int idCliente)
        {
            try
            {
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    return AfiliacionesSkEntities.Cliente.FirstOrDefault(x => x.IdCliente == idCliente);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Cliente CrearCliente(Cliente cliente)
        {
            try
            {
                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    AfiliacionesSkEntities.Cliente.Add(cliente);
                    AfiliacionesSkEntities.SaveChanges();
                }

                return this.ObtenerClientePorDocumento(cliente.TipoDocumento, cliente.NumeroDocumento);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Cliente ActualizarCliente(Cliente cliente)
        {
            try
            {
                var cli = new Cliente()
                {
                    IdCliente = -1
                };

                using (AfiliacionesSkEntities = new AfiliacionesSkandiaEntities())
                {
                    cli = AfiliacionesSkEntities.Cliente.FirstOrDefault(x => x.IdCliente == cliente.IdCliente);
                    cli.ApellidosCliente = cliente.ApellidosCliente;
                    cli.CiudadResidencia = cliente.CiudadResidencia;
                    cli.DepartamentoResidencia = cliente.DepartamentoResidencia;
                    cli.DireccionResidencia = cliente.DireccionResidencia;
                    cli.Discapacidad = cliente.Discapacidad;
                    cli.Egresos = cliente.Egresos;
                    cli.Email = cliente.Email;
                    cli.EstadoCivil = cliente.EstadoCivil;
                    cli.FechaNacimiento = cliente.FechaNacimiento;
                    cli.Genero = cliente.Genero;
                    cli.Ingresos = cliente.Ingresos;
                    cli.LugarNacimiento = cliente.LugarNacimiento;
                    cli.Movil = cliente.Movil;
                    cli.NombresCliente = cliente.NombresCliente;
                    cli.NumeroDocumento = cliente.NumeroDocumento;
                    cli.TipoDocumento = cliente.TipoDocumento;

                    AfiliacionesSkEntities.SaveChanges();
                }

                return this.ObtenerClientePorIdCliente(cliente.IdCliente);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}