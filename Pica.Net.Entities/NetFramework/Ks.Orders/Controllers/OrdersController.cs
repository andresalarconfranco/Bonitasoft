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
using Ks.Orders.Models;

namespace Ks.Orders.Controllers
{
    public class OrdersController : ApiController
    {
        private KallSonysEntities db = new KallSonysEntities();

        // GET: api/Orders
        public IQueryable<Models.Orders> GetOrders()
        {
            return db.Orders;
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Models.Orders))]
        public IHttpActionResult GetOrders(string id)
        {
            Models.Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrders(string id, Models.Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orders.OrdId)
            {
                return BadRequest();
            }

            db.Entry(orders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(Models.Orders))]
        public IHttpActionResult PostOrders(Models.Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(orders);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrdersExists(orders.OrdId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orders.OrdId }, orders);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Models.Orders))]
        public IHttpActionResult DeleteOrders(string id)
        {
            Models.Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }

            db.Orders.Remove(orders);
            db.SaveChanges();

            return Ok(orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdersExists(string id)
        {
            return db.Orders.Count(e => e.OrdId == id) > 0;
        }
    }
}