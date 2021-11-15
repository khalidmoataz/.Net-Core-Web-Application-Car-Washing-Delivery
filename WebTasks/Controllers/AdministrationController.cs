using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTasks.Data;
using WebTasks.Models;

namespace WebTasks.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdministrationController : Controller
    {
        private readonly AuthDbContext _db;
        private readonly RoleManager<IdentityRole> roleManager;

 
        public AdministrationController(RoleManager<IdentityRole> roleManager, AuthDbContext db)
        {
            this.roleManager = roleManager;
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Departments()
        {
            IEnumerable<Department> deps = _db.Departments;
            return View(deps);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Shop()
        {
            IEnumerable<Shop> items = _db.Shops;
            return View(items);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult push_item(Shop obj)
        {
            if (ModelState.IsValid)
            {
                _db.Shops.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Shop");
        }
        [HttpGet]
        public IActionResult delete_item(int id)
        {
            var obj = _db.Shops.Find(id);
            _db.Shops.Remove(obj);
            _db.SaveChanges();
           
            return RedirectToAction("Shop");
        }
        [HttpGet]
        public IActionResult Delete_app(int id)
        {
            var obj = _db.Appointments.Find(id);
            _db.Appointments.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("orders");
        }
        [HttpGet]
        public IActionResult Orders()
        {
            IEnumerable<Appointment> appointments = _db.Appointments;
            return View(appointments);
        }
        [HttpGet]
        public IActionResult edit_item(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Shops.Find(id);

            return View(obj);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult update_item(Shop obj)
        {
            if (ModelState.IsValid)
            {
                _db.Shops.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Shop");
        }
        public IActionResult add_item()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}