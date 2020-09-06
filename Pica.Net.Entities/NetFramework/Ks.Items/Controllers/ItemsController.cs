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
using Ks.Items.Models;

namespace Ks.Items.Controllers
{
    public class ItemsController : ApiController
    {
        private KallSonysEntities db = new KallSonysEntities();

        // GET: api/Items
        public IQueryable<Models.Items> GetItems()
        {
            return db.Items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Models.Items))]
        public IHttpActionResult GetItems(string id)
        {
            Models.Items items = db.Items.Find(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItems(string id, Models.Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != items.ItemId)
            {
                return BadRequest();
            }

            db.Entry(items).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(id))
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

        // POST: api/Items
        [ResponseType(typeof(Models.Items))]
        public IHttpActionResult PostItems(Models.Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(items);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ItemsExists(items.ItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = items.ItemId }, items);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Models.Items))]
        public IHttpActionResult DeleteItems(string id)
        {
            Models.Items items = db.Items.Find(id);
            if (items == null)
            {
                return NotFound();
            }

            db.Items.Remove(items);
            db.SaveChanges();

            return Ok(items);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemsExists(string id)
        {
            return db.Items.Count(e => e.ItemId == id) > 0;
        }
    }
}