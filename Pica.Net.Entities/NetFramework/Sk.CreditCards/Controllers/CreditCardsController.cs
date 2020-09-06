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
using Ks.CreditCards.Models;

namespace Ks.CreditCards.Controllers
{
    public class CreditCardsController : ApiController
    {
        private KallSonysEntities db = new KallSonysEntities();

        // GET: api/CreditCards
        public IQueryable<Models.CreditCards> GetCreditCards()
        {
            return db.CreditCards;
        }

        // GET: api/CreditCards/5
        [ResponseType(typeof(Models.CreditCards))]
        public IHttpActionResult GetCreditCards(int id)
        {
            Models.CreditCards creditCards = db.CreditCards.Find(id);
            if (creditCards == null)
            {
                return NotFound();
            }

            return Ok(creditCards);
        }

        // PUT: api/CreditCards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCreditCards(int id, Models.CreditCards creditCards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != creditCards.CrediCardNumber)
            {
                return BadRequest();
            }

            db.Entry(creditCards).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardsExists(id))
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

        // POST: api/CreditCards
        [ResponseType(typeof(Models.CreditCards))]
        public IHttpActionResult PostCreditCards(Models.CreditCards creditCards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CreditCards.Add(creditCards);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CreditCardsExists(creditCards.CrediCardNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = creditCards.CrediCardNumber }, creditCards);
        }

        // DELETE: api/CreditCards/5
        [ResponseType(typeof(Models.CreditCards))]
        public IHttpActionResult DeleteCreditCards(int id)
        {
            Models.CreditCards creditCards = db.CreditCards.Find(id);
            if (creditCards == null)
            {
                return NotFound();
            }

            db.CreditCards.Remove(creditCards);
            db.SaveChanges();

            return Ok(creditCards);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CreditCardsExists(int id)
        {
            return db.CreditCards.Count(e => e.CrediCardNumber == id) > 0;
        }
    }
}