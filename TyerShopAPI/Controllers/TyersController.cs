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
using TyerShopAPI.Models;

namespace TyerShopAPI.Controllers
{
    public class TyersController : ApiController
    {
        private TyerShopDbEntities db = new TyerShopDbEntities();

        // GET: api/Tyers
        public IQueryable<Tyer> GetTyers()
        {
            return db.Tyers;
        }

        // GET: api/Tyers/5
        [ResponseType(typeof(Tyer))]
        public IHttpActionResult GetTyer(int id)
        {
            Tyer tyer = db.Tyers.Find(id);
            if (tyer == null)
            {
                return NotFound();
            }

            return Ok(tyer);
        }

        // PUT: api/Tyers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTyer(int id, Tyer tyer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tyer.TyerId)
            {
                return BadRequest();
            }

            db.Entry(tyer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TyerExists(id))
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

        // POST: api/Tyers
        [ResponseType(typeof(Tyer))]
        public IHttpActionResult PostTyer(Tyer tyer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tyers.Add(tyer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tyer.TyerId }, tyer);
        }

        // DELETE: api/Tyers/5
        [ResponseType(typeof(Tyer))]
        public IHttpActionResult DeleteTyer(int id)
        {
            Tyer tyer = db.Tyers.Find(id);
            if (tyer == null)
            {
                return NotFound();
            }

            db.Tyers.Remove(tyer);
            db.SaveChanges();

            return Ok(tyer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TyerExists(int id)
        {
            return db.Tyers.Count(e => e.TyerId == id) > 0;
        }
    }
}