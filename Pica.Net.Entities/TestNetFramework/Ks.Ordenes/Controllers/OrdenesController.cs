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
using Ks.Ordenes.Models;

namespace Ks.Ordenes.Controllers
{
    public class OrdenesController : ApiController
    {
        private KallSonysEntities5 db = new KallSonysEntities5();

        // GET: api/Ordenes
        public IQueryable<Models.Ordenes> GetOrdenes()
        {
            return db.Ordenes;
        }

        // GET: api/Ordenes/5
        [ResponseType(typeof(Models.Ordenes))]
        public IHttpActionResult GetOrdenes(int id)
        {
            Models.Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return NotFound();
            }

            return Ok(ordenes);
        }

        // PUT: api/Ordenes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrdenes(int id, Models.Ordenes ordenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ordenes.ordenId)
            {
                return BadRequest();
            }

            db.Entry(ordenes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenesExists(id))
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

        // POST: api/Ordenes
        [ResponseType(typeof(Models.Ordenes))]
        public IHttpActionResult PostOrdenes(Models.Ordenes ordenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ordenes.Add(ordenes);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrdenesExists(ordenes.ordenId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ordenes.ordenId }, ordenes);
        }

        // DELETE: api/Ordenes/5
        [ResponseType(typeof(Models.Ordenes))]
        public IHttpActionResult DeleteOrdenes(int id)
        {
            Models.Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return NotFound();
            }

            db.Ordenes.Remove(ordenes);
            db.SaveChanges();

            return Ok(ordenes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdenesExists(int id)
        {
            return db.Ordenes.Count(e => e.ordenId == id) > 0;
        }
    }
}