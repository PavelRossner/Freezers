using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mrazaky.Data;
using Mrazaky.Models;

namespace Mrazaky.Controllers
{
    public class FreezerFoodsController : Controller
    {
        private readonly ApplicationDbContext db;

        public FreezerFoodsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: FreezerFoods
        public async Task<IActionResult> Index()
        {
            var user = User.Identity.GetUserId();
            string email = User.Identity.Name;

            if (user == null)
            {
                return RedirectToAction("Error_user");
            }

            else if (email.Contains("admin") == true || email.Contains("pajaro") == true)
            {
                var applicationDbContext = db.FreezerFood.Include(f => f.Food).Include(f => f.Freezer);
                return View(await applicationDbContext.ToListAsync());
            }

            else
            {
                return RedirectToAction("Error_user");
            }
        }

        // GET: FreezerFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.FreezerFood == null)
            {
                return NotFound();
            }

            var freezerFood = await db.FreezerFood
                .Include(f => f.Food)
                .Include(f => f.Freezer)
                .FirstOrDefaultAsync(m => m.FreezerFoodID == id);
            if (freezerFood == null)
            {
                return NotFound();
            }

            return View(freezerFood);
        }

        // GET: FreezerFoods/Create
        public IActionResult Create()
        {
            ViewData["FoodID"] = new SelectList(db.Food, "FoodId", "FoodId");
            ViewData["FreezerID"] = new SelectList(db.Freezer, "FreezerId", "FreezerId");
            return View();
        }

        // POST: FreezerFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FreezerFoodID,FoodID,FreezerID,FreezerLocation")] FreezerFood freezerFood)
        {
            if (ModelState.IsValid)
            {
                db.Add(freezerFood);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodID"] = new SelectList(db.Food, "FoodId", "FoodId", freezerFood.FoodID);
            ViewData["FreezerID"] = new SelectList(db.Freezer, "FreezerId", "FreezerId", freezerFood.FreezerID);
            return View(freezerFood);
        }

        // GET: FreezerFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.FreezerFood == null)
            {
                return NotFound();
            }

            var freezerFood = await db.FreezerFood.FindAsync(id);
            if (freezerFood == null)
            {
                return NotFound();
            }
            ViewData["FoodID"] = new SelectList(db.Food, "FoodId", "FoodId", freezerFood.FoodID);
            ViewData["FreezerID"] = new SelectList(db.Freezer, "FreezerId", "FreezerId", freezerFood.FreezerID);
            return View(freezerFood);
        }

        // POST: FreezerFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FreezerFoodID,FoodID,FreezerID,FreezerLocation")] FreezerFood freezerFood)
        {
            if (id != freezerFood.FreezerFoodID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(freezerFood);
            }

            ViewData["FoodID"] = new SelectList(db.Food, "FoodId", "FoodId", freezerFood.FoodID);
            ViewData["FreezerID"] = new SelectList(db.Freezer, "FreezerId", "FreezerId", freezerFood.FreezerID);
            db.Update(freezerFood);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: FreezerFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.FreezerFood == null)
            {
                return NotFound();
            }

            var freezerFood = await db.FreezerFood
                .Include(f => f.Food)
                .Include(f => f.Freezer)
                .FirstOrDefaultAsync(m => m.FreezerFoodID == id);
            if (freezerFood == null)
            {
                return NotFound();
            }

            return View(freezerFood);
        }

        // POST: FreezerFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.FreezerFood == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FreezerFood'  is null.");
            }
            var freezerFood = await db.FreezerFood.FindAsync(id);
            if (freezerFood != null)
            {
                db.FreezerFood.Remove(freezerFood);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error_admin()
        {
            return View();
        }

        public IActionResult Error_user()
        {
            return View();
        }
    }
}
