using Rambars.Models;
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
using System.Web.Mvc;

namespace Rambars.Controllers.Mvc
{
    public class BarsController : Controller
    {
        private RambarsContext db = new RambarsContext();

        // GET: Bars1
        public async Task<ActionResult> Index()
        {
            return View(await db.Bars.ToListAsync());
        }

        // GET: Bars1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = await db.Bars.FindAsync(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // GET: Bars1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bars1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,AddressLineOne,AddressLineTwo,City,ZipCode")] Bar bar)
        {
            if (ModelState.IsValid)
            {
                db.Bars.Add(bar);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bar);
        }

        // GET: Bars1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = await db.Bars.FindAsync(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // POST: Bars1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AddressLineOne,AddressLineTwo,City,ZipCode")] Bar bar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bar).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bar);
        }

        // GET: Bars1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = await db.Bars.FindAsync(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // POST: Bars1/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bar bar = await db.Bars.FindAsync(id);
            db.Bars.Remove(bar);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

namespace Rambars.Controllers
{
    public class BarsController : ApiController
    {
        private RambarsContext db = new RambarsContext();

        // GET: api/Bars
        public IQueryable<BarDTO> GetBars()
        {
            var bars = from b in db.Bars
                       select new BarDTO()
                       {
                           Id = b.Id,
                           Name = b.Name,
                           Specials = db.Specials.Where(x => x.BarId == b.Id).ToList()
                       };

            return bars;
        }

        // GET: api/Bars/5
        [ResponseType(typeof(BarDTO))]
        public async Task<IHttpActionResult> GetBar(int id)
        {
            var bar = await db.Bars.Select(x =>
                new BarDetailDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    AddressLineOne = x.AddressLineOne,
                    AddressLineTwo = x.AddressLineTwo,
                    City = x.City,
                    ZipCode = x.ZipCode,
                    Specials = db.Specials.Where(s => s.Id == x.Id).ToList()
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                return NotFound();
            }

            return Ok(bar);
        }

        // PUT: api/Bars/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBar(int id, Bar bar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bar.Id)
            {
                return BadRequest();
            }

            db.Entry(bar).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarExists(id))
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

        // POST: api/Bars
        [ResponseType(typeof(Bar))]
        public async Task<IHttpActionResult> PostBar(Bar bar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bars.Add(bar);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bar.Id }, bar);
        }

        // DELETE: api/Bars/5
        [ResponseType(typeof(Bar))]
        public async Task<IHttpActionResult> DeleteBar(int id)
        {
            Bar bar = await db.Bars.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }

            db.Bars.Remove(bar);
            await db.SaveChangesAsync();

            return Ok(bar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BarExists(int id)
        {
            return db.Bars.Count(e => e.Id == id) > 0;
        }
    }
}