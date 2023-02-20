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
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext db;

        public FoodsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Foods
        public IActionResult Index()
        {
            string Id = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == Id);    

            if (user == null)
            {
                return RedirectToAction("Error", "Foods");
            }

            return View(db.Food.Where(u => u.User.Id == Id).Select(FoodResponse.GetFoodResponse).ToList());
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            bool freezer = db.Freezer.Any(fr => fr.FreezerName != null);

            if (freezer == false)
            {
                return RedirectToAction("Error_Food");
            }

            else
                ViewData["Category"] = new SelectList(db.FoodCategory, "FoodCategoryName", "FoodCategoryName");     //Creates dropdown menu in View (also see Create/POST)
            ViewData["FreezerName"] = new SelectList(db.Freezer, "FreezerName", "FreezerName");                 //Creates dropdown menu in View (also see Create/POST)
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Category,FoodName,FreezerName,NumberOfPackages,Weight,PackageLabel,FreezerPosition,DatePurchase,BestBefore,Note")] Food food, Freezer freezer, FreezerFood freezerFood, FoodCategory foodCategory)
        {

            if (!ModelState.IsValid)
            {
                return View(food);
            }


            if (ModelState.IsValid)
            {
                var user = db.User.FirstOrDefault(u => u.Id == this.User.Identity.GetUserId());
                user.Foods.Add(food);                                                                       //creates FoodId in database table Food, foreign key ApplicationUserId
                db.SaveChanges();

                freezer = db.Freezer.Where(fr => fr.FreezerName == food.FreezerName).FirstOrDefault();      //creates foreign key FreezerId in database table Food

                if (freezer == null)
                {
                    return RedirectToAction("Error_Food");
                }

                else

                    freezer.Foods.Add(food);

                freezer.FreezerFood.Add(freezerFood);                                                       //creates FreezerID in database table FreezerFood, primary key FreezerFoodID
                food.FreezerFood.Add(freezerFood);                                                          //creates FoodID in database table FreezerFood, primary key FreezerFoodID

                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewData["Category"] = new SelectList(db.FoodCategory, "FoodCategoryName", "FoodCategoryName", foodCategory.FoodCategoryName);  //Creates dropdown menu in View (also see Create/GET)
            ViewData["FreezerName"] = new SelectList(db.Freezer, "FreezerName", "FreezerName", freezerFood.FreezerName);                    //Creates dropdown menu in View (also see Create/GET)
            return View(food);
        }


        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Food == null)
            {
                return NotFound();
            }

            Food food = await db.Food.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }
            ViewData["Category"] = new SelectList(db.FoodCategory, "FoodCategoryName", "FoodCategoryName");     //Creates dropdown menu in View (also see Create/POST)
            ViewData["FreezerName"] = new SelectList(db.Freezer, "FreezerName", "FreezerName");                 //Creates dropdown menu in View (also see Create/POST)
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Food food, FreezerFood freezerFood, FoodCategory foodCategory)
        {
            if (id != food.FoodId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(food);
            }

            Food dbFood = db.Food.FirstOrDefault(fo => fo.FoodId == id);

            if (dbFood == null)
            {
                return View(food);
            }

            dbFood.Category = food.Category;
            dbFood.FoodName = food.FoodName;
            dbFood.FreezerName = food.FreezerName;
            dbFood.NumberOfPackages = food.NumberOfPackages;
            dbFood.Weight = food.Weight;
            dbFood.PackageLabel = food.PackageLabel;
            dbFood.FreezerPosition = food.FreezerPosition;
            dbFood.DatePurchase = food.DatePurchase;
            dbFood.BestBefore = food.BestBefore;
            dbFood.Note = food.Note;

            db.SaveChanges();
            ViewData["Category"] = new SelectList(db.FoodCategory, "FoodCategoryName", "FoodCategoryName", foodCategory.FoodCategoryName);  //Creates dropdown menu in View (also see Create/GET)
            ViewData["FreezerName"] = new SelectList(db.Freezer, "FreezerName", "FreezerName", freezerFood.FreezerName);                    //Creates dropdown menu in View (also see Create/GET)
            return RedirectToAction(nameof(Index));
        }


        // GET: Foods/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || db.Food == null)
            {
                return NotFound();
            }

            FoodResponse food = db.Food.Include(fr => fr.FreezerFood)
                                .Select(FoodResponse.GetFoodResponse)
                                .FirstOrDefault(f => f.FoodId == id);

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Food == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Food'  is null.");
            }
            Food food = await db.Food.FindAsync(id);
            if (food != null)
            {
                db.Food.Remove(food);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return db.Food.Any(e => e.FoodId == id);
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Error_Food()
        {
            return View();
        }
    }
}
