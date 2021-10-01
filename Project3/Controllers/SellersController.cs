using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Controllers
{
    public class SellersController : Controller
    {
        private readonly ProjectContext _context;
        AvailableLaptopsController a;
        public SellersController(ProjectContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Sname,Spass")]TblSeller seller)
        {
            var user = _context.TblSeller.Single(x => x.Sname == seller.Sname);
            if (user != null)
            {
                if (user.Spass == seller.Spass)
                {
                    ViewBag.msg = "Valid Credentials";
                    return RedirectToAction("Dashboard");
                }
                else
                    ViewBag.msg = "Invalid Credentials";


            }
            else
                ViewBag.msg = "Invalid Credentials";

            return View();
        }
        // GET: Sellers
        public async Task<IActionResult> Index()
        {
            // return View(await _context.TblSeller.ToListAsync());
            return RedirectToAction("Login");
        }


        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> PostLaptop()
        {
            return RedirectToRoute(new
            {
                controller = "AvailableLaptops",
                action = "Create",
                
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

        
        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSeller = await _context.TblSeller
                .FirstOrDefaultAsync(m => m.Sid == id);
            if (tblSeller == null)
            {
                return NotFound();
            }

            return View(tblSeller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sid,Sname,Spass,Scontact,Semail")] TblSeller tblSeller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblSeller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblSeller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSeller = await _context.TblSeller.FindAsync(id);
            if (tblSeller == null)
            {
                return NotFound();
            }
            return View(tblSeller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sid,Sname,Spass,Scontact,Semail")] TblSeller tblSeller)
        {
            if (id != tblSeller.Sid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblSeller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSellerExists(tblSeller.Sid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblSeller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSeller = await _context.TblSeller
                .FirstOrDefaultAsync(m => m.Sid == id);
            if (tblSeller == null)
            {
                return NotFound();
            }

            return View(tblSeller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblSeller = await _context.TblSeller.FindAsync(id);
            _context.TblSeller.Remove(tblSeller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSellerExists(int id)
        {
            return _context.TblSeller.Any(e => e.Sid == id);
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
