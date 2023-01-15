using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mrazaky.Data;
using Mrazaky.Models;
using Mrazaky.Models.Response;

namespace Mrazaky.Controllers
{
    public class FreezersController : Controller
    {
        private readonly ApplicationDbContext db;

        public FreezersController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Freezers
        public IActionResult Index()
        {
            string user = User.Identity.GetUserId();

            if (user == null)
            {
                return RedirectToAction("Error", "Freezers");
            }

            var applicationDbContext = db.Freezer.Where(fr => fr.Id == user || User.Identity.Name.Contains("admin")).Include(fr => fr.User).Include(fo => fo.Foods);
            return View(db.Freezer.Select(FreezerResponse.GetFreezerResponse).ToList());
        }


        // GET: Freezers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Freezers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FreezerId,Id,Order,FreezerLocation,NumberOfShelves,LastDefrosted,DefrostInterval,NextDefrosted,DaysToDefrost,Note")] Freezer freezer, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                user = db.User.FirstOrDefault(u => u.Id == this.User.Identity.GetUserId());

                user.Freezers.Add(freezer);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Id"] = new SelectList(db.User, "Id", "Id", freezer.Id);
            return View(freezer);
        }

        // GET: Freezers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Freezer == null)
            {
                return NotFound();
            }

            Freezer freezer = await db.Freezer.FindAsync(id);
            if (freezer == null)
            {
                return NotFound();
            }
            //ViewData["Id"] = new SelectList(db.User, "Id", "Id", freezer.Id);
            return View(freezer);
        }

        // POST: Freezers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Freezer freezer)
        {
            if (id != freezer.FreezerId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(freezer);
            }

            Freezer dbFreezer = db.Freezer.FirstOrDefault(fr => fr.FreezerId == id);
            
            if (dbFreezer == null)
            {
                return View(freezer);
            }

            dbFreezer.Order = freezer.Order;
            dbFreezer.FreezerLocation = freezer.FreezerLocation;
            dbFreezer.NumberOfShelves = freezer.NumberOfShelves;
            dbFreezer.LastDefrosted = freezer.LastDefrosted;
            dbFreezer.DefrostInterval = freezer.DefrostInterval;
            dbFreezer.Note = freezer.Note;

            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        // GET: Freezers/Delete/5
        public IActionResult Delete(int? id, string FreezerLocation, string Freezer)
        {
            if (id == null || db.Freezer == null)
            {
                return NotFound();
            }

            var freezerfreezerlocation = db.Freezer.Select(fl => fl.FreezerLocation);
            var foodfreezer = db.Food.Select(fr => fr.Freezer);

            if (freezerfreezerlocation == foodfreezer)
            {
                return RedirectToAction("Error_database_freezer", "Freezers");
            }

            FreezerResponse freezer = db.Freezer.Include(fr => fr.User)
                                      .Select(FreezerResponse.GetFreezerResponse)
                                      .FirstOrDefault(f => f.FreezerId == id);
            
            if (freezer == null)
            {
                return NotFound();
            }

            return View(freezer);
        }

        // POST: Freezers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Freezer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Freezer'  is null.");
            }
            var freezer = await db.Freezer.FindAsync(id);
            if (freezer != null)
            {
                db.Freezer.Remove(freezer);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FreezerExists(int id)
        {
          return db.Freezer.Any(e => e.FreezerId == id);
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Error_database_freezer()
        {
            return View();
        }
    }
}
