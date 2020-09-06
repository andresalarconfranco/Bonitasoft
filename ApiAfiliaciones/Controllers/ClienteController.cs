using ApiAfiliaciones.Dal;
using System;
using System.Web.Http;

namespace ApiAfiliaciones.Controllers
{
    public class ClienteController : ApiController
    {
        [HttpGet]
        public Cliente Get(int idCliente)
        {
            try
            {
                ClienteDal clienteDal = new ClienteDal();
                var cli = clienteDal.ObtenerClientePorIdCliente(idCliente);
                return cli;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public Cliente Get(string tipoDoc, string numDoc)
        {
            try
            {
                ClienteDal clienteDal = new ClienteDal();
                var cli = clienteDal.ObtenerClientePorDocumento(tipoDoc, numDoc);
                return cli;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public Cliente Post([FromBody]Cliente cliente)
        {
            try
            {
                ClienteDal clienteDal = new ClienteDal();
                var cli = clienteDal.CrearCliente(cliente);
                return cli;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
