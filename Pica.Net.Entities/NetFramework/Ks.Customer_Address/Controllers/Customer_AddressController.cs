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
using Ks.Customer_Address.Models;

namespace Ks.Customer_Address.Controllers
{
    public class Customer_AddressController : ApiController
    {
        private KallSonysEntities db = new KallSonysEntities();

        // GET: api/Customer_Address
        public IQueryable<Models.Customer_Address> GetCustomer_Address()
        {
            return db.Customer_Address;
        }

        // GET: api/Customer_Address/5
        [ResponseType(typeof(Models.Customer_Address))]
        public IHttpActionResult GetCustomer_Address(string id)
        {
            Models.Customer_Address customer_Address = db.Customer_Address.Find(id);
            if (customer_Address == null)
            {
                return NotFound();
            }

            return Ok(customer_Address);
        }

        // PUT: api/Customer_Address/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer_Address(string id, Models.Customer_Address customer_Address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer_Address.CustId)
            {
                return BadRequest();
            }

            db.Entry(customer_Address).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Customer_AddressExists(id))
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

        // POST: api/Customer_Address
        [ResponseType(typeof(Models.Customer_Address))]
        public IHttpActionResult PostCustomer_Address(Models.Customer_Address customer_Address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer_Address.Add(customer_Address);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Customer_AddressExists(customer_Address.CustId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customer_Address.CustId }, customer_Address);
        }

        // DELETE: api/Customer_Address/5
        [ResponseType(typeof(Models.Customer_Address))]
        public IHttpActionResult DeleteCustomer_Address(string id)
        {
            Models.Customer_Address customer_Address = db.Customer_Address.Find(id);
            if (customer_Address == null)
            {
                return NotFound();
            }

            db.Customer_Address.Remove(customer_Address);
            db.SaveChanges();

            return Ok(customer_Address);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Customer_AddressExists(string id)
        {
            return db.Customer_Address.Count(e => e.CustId == id) > 0;
        }
    }
}