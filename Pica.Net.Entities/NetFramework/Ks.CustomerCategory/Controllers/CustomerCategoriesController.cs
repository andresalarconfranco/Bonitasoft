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
using Ks.CustomerCategory.Models;

namespace Ks.CustomerCategory.Controllers
{
    public class CustomerCategoriesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/CustomerCategories
        public IQueryable<Models.CustomerCategory> GetCustomerCategory()
        {
            return db.CustomerCategory;
        }

        // GET: api/CustomerCategories/5
        [ResponseType(typeof(Models.CustomerCategory))]
        public IHttpActionResult GetCustomerCategory(string id)
        {
            Models.CustomerCategory customerCategory = db.CustomerCategory.Find(id);
            if (customerCategory == null)
            {
                return NotFound();
            }

            return Ok(customerCategory);
        }

        // PUT: api/CustomerCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerCategory(string id, Models.CustomerCategory customerCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerCategory.IdCategory)
            {
                return BadRequest();
            }

            db.Entry(customerCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerCategoryExists(id))
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

        // POST: api/CustomerCategories
        [ResponseType(typeof(Models.CustomerCategory))]
        public IHttpActionResult PostCustomerCategory(Models.CustomerCategory customerCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerCategory.Add(customerCategory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerCategoryExists(customerCategory.IdCategory))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customerCategory.IdCategory }, customerCategory);
        }

        // DELETE: api/CustomerCategories/5
        [ResponseType(typeof(Models.CustomerCategory))]
        public IHttpActionResult DeleteCustomerCategory(string id)
        {
            Models.CustomerCategory customerCategory = db.CustomerCategory.Find(id);
            if (customerCategory == null)
            {
                return NotFound();
            }

            db.CustomerCategory.Remove(customerCategory);
            db.SaveChanges();

            return Ok(customerCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerCategoryExists(string id)
        {
            return db.CustomerCategory.Count(e => e.IdCategory == id) > 0;
        }
    }
}