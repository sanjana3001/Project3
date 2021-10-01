using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProjectContext _context;

        public AdminController(ProjectContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Aname,Apass")] TblAdmin admin)
        {
            var user = _context.TblAdmin.Single(x => x.Aname == admin.Aname);
            if (user != null)
            {
                if (user.Apass == admin.Apass)
                {
                    ViewBag.msg = "Valid Credentials";
                    return RedirectToAction("Dashboard", user.Aid);
                }
                else
                    ViewBag.msg = "Invalid Credentials";


            }
            else
                ViewBag.msg = "Invalid Credentials";

            return View();
        }
        // GET: AdminController
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult GetAllLaptop()
        {
            return RedirectToRoute(new
            {
                controller = "AvailableLaptops",
                action = "Index",

            });
        }

        public async Task<IActionResult> SaleReport()
        {
            return RedirectToRoute(new
            {
                controller = "OrderedLaptops",
                action = "Index"
            });
        }
        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Logout()
        {
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index"
            });
        }
    }
}
