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
using System.Linq;

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
            string Id = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == Id);

            if (user == null)
            {
                return RedirectToAction("Error", "Freezers");
            }

            return View(db.Freezer.Where(u => u.User.Id == Id).Select(FreezerResponse.GetFreezerResponse).ToList());
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
        public IActionResult Create([Bind("FreezerId,FreezerName,NumberOfShelves,LastDefrosted,DefrostInterval,Note")] Freezer freezer, ApplicationUserFreezer applicationUserFreezer, ApplicationUser applicationUser, FreezerFood freezerFood, Food food, int? id)
        {
            if (!ModelState.IsValid)
            {
                return View(freezer);
            }


            if (ModelState.IsValid)
            {
                var user = db.User.FirstOrDefault(u => u.Id == this.User.Identity.GetUserId());
                user.Freezers.Add(freezer);                         //vytvoří FreezerId v tabulce Freezer, cizí klíč ApplicationUserId
                db.SaveChanges();

                freezer.UserFreezer.Add(applicationUserFreezer);    //vytvoří FreezerId v tabulce ApplicationUserFreezer, primární klíč ApplicationUserFreezerId
                user.UserFreezer.Add(applicationUserFreezer);       //vytvoří ApplicationUserId v tabulce ApplicationUserFreezer, primární klíč ApplicationUserFreezerId                
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
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

            dbFreezer.FreezerName = freezer.FreezerName;
            dbFreezer.NumberOfShelves = freezer.NumberOfShelves;
            dbFreezer.LastDefrosted = freezer.LastDefrosted;
            dbFreezer.DefrostInterval = freezer.DefrostInterval;
            dbFreezer.Note = freezer.Note;

            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        // GET: Freezers/Delete/5
        public IActionResult Delete(int? id, Food food)
        {
            if (id == null || db.Freezer == null)
            {
                return NotFound();
            }

            FreezerResponse freezer = db.Freezer.Include(fr => fr.UserFreezer)
                                      .Select(FreezerResponse.GetFreezerResponse)
                                      .FirstOrDefault(f => f.FreezerId == id);

            food = db.Food.Where(fo => fo.FreezerName == freezer.FreezerName).FirstOrDefault();

            if (food != null)
            {
                return RedirectToAction("Error_database_freezer");
            }

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
            Freezer freezer = await db.Freezer.FindAsync(id);

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

        public IActionResult Error_Freezer()
        {
            return View();
        }

        public IActionResult Error_database_freezer()
        {
            return View();
        }
    }
}
