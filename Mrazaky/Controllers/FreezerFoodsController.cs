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
        public IActionResult Index()
        {
            string Id = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == Id);

            if (user == null)
            {
                return RedirectToAction("Error_user");
            }

            return View(db.FreezerFood.Where(u => u.User.Id == Id).Include(f => f.Food).Include(f => f.Freezer).ToList());
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
