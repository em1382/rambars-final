using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Rambars.Models;

namespace Rambars.Controllers
{
    public class SpecialsController : ApiController
    {
        private RambarsContext db = new RambarsContext();

        // GET: api/Specials
        public IQueryable<SpecialDTO> GetSpecials()
        {
            var specials = from s in db.Specials
                           select new SpecialDTO()
                           {
                               Id = s.Id,
                               Name = s.Name,
                               BarName = s.Bar.Name
                           };

            return specials;
        }

        // GET: api/Specials/5
        [ResponseType(typeof(SpecialDetailDTO))]
        public async Task<IHttpActionResult> GetSpecial(int id)
        {
            var special = await db.Specials.Include(s => s.Bar).Select(s =>
                new SpecialDetailDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price.ToString("C"),
                    BarName = s.Bar.Name,
                    StartTime = s.StartTime.ToString("MM/dd/yy hh:mm"),
                    EndTime = s.EndTime.ToString("MM/dd/yy hh:mm"),
                }).SingleOrDefaultAsync(s => s.Id == id);

            if (special == null)
            {
                return NotFound();
            }

            return Ok(special);
        }

        // PUT: api/Specials/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSpecial(int id, Special special)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != special.Id)
            {
                return BadRequest();
            }

            db.Entry(special).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialExists(id))
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

        // POST: api/Specials
        [ResponseType(typeof(Special))]
        public async Task<IHttpActionResult> PostSpecial(Special special)
        {
            special.StartTime = DateTime.Now;
            special.EndTime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Specials.Add(special);
            await db.SaveChangesAsync();

            db.Entry(special).Reference(x => x.Bar).Load();

            var dto = new SpecialDTO()
            {
                Id = special.Id,
                Name = special.Name,
                BarName = special.Bar.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = special.Id }, dto);
        }

        // DELETE: api/Specials/5
        [ResponseType(typeof(Special))]
        public async Task<IHttpActionResult> DeleteSpecial(int id)
        {
            Special special = await db.Specials.FindAsync(id);
            if (special == null)
            {
                return NotFound();
            }

            db.Specials.Remove(special);
            await db.SaveChangesAsync();

            return Ok(special);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpecialExists(int id)
        {
            return db.Specials.Count(e => e.Id == id) > 0;
        }
    }
}