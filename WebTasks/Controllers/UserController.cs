using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTasks.Data;
using WebTasks.Models;
using WebTasks.Areas.Identity.Data;

namespace WebTasks.Controllers
{
    [Authorize(Roles = "Member")]
    public class UserController : Controller
    {
        private readonly AuthDbContext _db;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<WebTasksUser> userManager;

      
        public UserController(UserManager<WebTasksUser> userManager,RoleManager<IdentityRole> roleManager, AuthDbContext db)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _db = db;
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Appointment()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Orders()
        {

            IEnumerable<Appointment> appointments = _db.Appointments.Where(Appointment => Appointment.user_id == userManager.GetUserId(User));
            return View(appointments);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Appointment(Appointment obj)
        {
            obj.status = "Pending";
            _db.Appointments.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult Edit_app(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Appointments.Find(id);
          
            return View(obj);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit_app(Appointment obj)
        {
            if (ModelState.IsValid)
            {
                _db.Appointments.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Orders");
        }
        [HttpGet]
        public IActionResult Delete_app(int? id)
        {
            var obj = _db.Appointments.Find(id);

            _db.Appointments.Remove(obj);
                _db.SaveChanges();
            
            return RedirectToAction("Orders");
        }
    }
}
