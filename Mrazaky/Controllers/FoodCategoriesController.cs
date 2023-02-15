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
    public class FoodCategoriesController : Controller
    {
        private readonly ApplicationDbContext db;

        public FoodCategoriesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: FoodCategories
        public IActionResult Index()
        {
            var user = User.Identity.GetUserId();

            if (user == null)
            {
                return RedirectToAction("Error_user", "Account");
            }

                return View(db.FoodCategory.ToList());
        }

        // GET: FoodCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodCategoryId,FoodCategoryName")] FoodCategory foodCategory)
        {
            if (ModelState.IsValid)
            {
                db.Add(foodCategory);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodCategory);
        }

        // GET: FoodCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.FoodCategory == null)
            {
                return NotFound();
            }

            var foodCategory = await db.FoodCategory.FindAsync(id);
            if (foodCategory == null)
            {
                return NotFound();
            }
            return View(foodCategory);
        }

        // POST: FoodCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FoodCategoryId,FoodCategoryName")] FoodCategory foodCategory)
        {
            if (id != foodCategory.FoodCategoryId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(foodCategory);
            }

            db.Update(foodCategory);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: FoodCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.FoodCategory == null)
            {
                return NotFound();
            }

            var foodCategory = await db.FoodCategory
                .FirstOrDefaultAsync(fc => fc.FoodCategoryId == id);
            if (foodCategory == null)
            {
                return NotFound();
            }

            return View(foodCategory);
        }

        // POST: FoodCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.FoodCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FoodCategory'  is null.");
            }
            var foodCategory = await db.FoodCategory.FindAsync(id);
            if (foodCategory != null)
            {
                db.FoodCategory.Remove(foodCategory);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error_user()
        {
            return View();
        }
    }
}
