using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Ks.Customer.Models;

namespace Ks.Customer.Controllers
{
    public class CustomersController : ApiController
    {
        private KallSonysEntities db = new KallSonysEntities();
        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<Models.Customer, Dto.CustomerDto>> AsCustomerDto =
            x => new Dto.CustomerDto
            {
                CustID = x.CustID,
                FName = x.FName,
                LName = x.LName,
                PhoneNumber = x.PhoneNumber,
                EMail = x.EMail,
                Password = x.Password,
                CreditCardType = x.CreditCardType,
                CrediCardNumber = x.CrediCardNumber,
                Status = x.Status,
                IdCategory = x.IdCategory,
                Country = x.Country,
                City = x.City,
                ROL_NCODE = x.ROL_NCODE
            };

        // GET: api/Customers
        public IQueryable<Models.Customer> GetCustomer()
        {
            return db.Customer;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Models.Customer))]
        public IHttpActionResult GetCustomer(string id)
        {
            Models.Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        // GET: api/CustomersByName/name
        [Route("~/api/CustomersByName/{name}")]
        public IQueryable<Dto.CustomerDto> GetCustomerByName(string name, string apellido)
        {
            return db.Customer.Include(b => b.FName)
                .Where(b => b.FName.Contains(name))
                .Select(AsCustomerDto);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(string id, Models.Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [ResponseType(typeof(Models.Customer))]
        public IHttpActionResult PostCustomer(Models.Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer.Add(customer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customer.CustID }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Models.Customer))]
        public IHttpActionResult DeleteCustomer(string id)
        {
            Models.Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customer.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(string id)
        {
            return db.Customer.Count(e => e.CustID == id) > 0;
        }
    }
}