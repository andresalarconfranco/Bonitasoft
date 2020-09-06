using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Ks.Clientes.Entities;

namespace Ks.Clientes.Controllers
{
    public class ClientesController : ApiController
    {
        private KallSonysEntities db = new KallSonysEntities();

        // GET: api/Clientes
        public IQueryable<Entities.Clientes> GetClientes()
        {
            return db.Clientes;
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Entities.Clientes))]
        public IHttpActionResult GetClientes(long id)
        {
            Entities.Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return NotFound();
            }

            return Ok(clientes);
        }

        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClientes(long id, Entities.Clientes clientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientes.Identificacion)
            {
                return BadRequest();
            }

            db.Entry(clientes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clientes
        [ResponseType(typeof(Entities.Clientes))]
        public IHttpActionResult PostClientes(Entities.Clientes clientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(clientes);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ClientesExists(clientes.Identificacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = clientes.Identificacion }, clientes);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Entities.Clientes))]
        public IHttpActionResult DeleteClientes(long id)
        {
            Entities.Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return NotFound();
            }

            db.Clientes.Remove(clientes);
            db.SaveChanges();

            return Ok(clientes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientesExists(long id)
        {
            return db.Clientes.Count(e => e.Identificacion == id) > 0;
        }
    }
}